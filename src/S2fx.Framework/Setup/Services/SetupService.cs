using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OrchardCore.DeferredTasks;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Builders;
using OrchardCore.Environment.Shell.Descriptor;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Environment.Shell.Models;
using OrchardCore.Modules;
using S2fx.Data;
using S2fx.Data.Importing.Seeds;
using S2fx.Setup.Events;
using S2fx.Setup.Model;
using S2fx.View.Data;

namespace S2fx.Setup.Services {

    public interface ISetupService {
        Task<string> SetupAsync(SetupContext context);
    }


    public class SetupService : ISetupService {
        private readonly IShellHost _shellHost;
        private readonly IShellContextFactory _shellContextFactory;
        private readonly ILogger _logger;
        private readonly IStringLocalizer T;
        private readonly IApplicationLifetime _applicationLifetime;
        private readonly string _applicationName;

        public SetupService(
            IShellHost shellHost,
            IHostingEnvironment hostingEnvironment,
            IShellContextFactory shellContextFactory,
            IRunningShellTable runningShellTable,
            ILogger<SetupService> logger,
            IStringLocalizerFactory stringLocalizerFactory,
            IStringLocalizer<SetupService> stringLocalizer,
            IApplicationLifetime applicationLifetime
            ) {
            _shellHost = shellHost;
            _applicationName = hostingEnvironment.ApplicationName;
            _shellContextFactory = shellContextFactory;
            _logger = logger;
            T = stringLocalizer;
            _applicationLifetime = applicationLifetime;
        }

        public async Task<string> SetupAsync(SetupContext context) {
            var initialState = context.ShellSettings.State;
            try {
                var executionId = await SetupInternalAsync(context);

                if (context.Errors.Any()) {
                    context.ShellSettings.State = initialState;
                }

                return executionId;
            }
            catch {
                context.ShellSettings.State = initialState;
                throw;
            }
        }

        public async Task<string> SetupInternalAsync(SetupContext context) {
            string executionId;

            if (_logger.IsEnabled(LogLevel.Information)) {
                _logger.LogInformation("Running setup for tenant '{TenantName}'.", context.ShellSettings.Name);
            }

            // Features to enable for Setup
            string[] hardcoded =
            {
                _applicationName,
                //TODO 
            };

            context.EnabledFeatures = hardcoded.Union(context.EnabledFeatures ?? Enumerable.Empty<string>()).Distinct().ToArray();

            // Set shell state to "Initializing" so that subsequent HTTP requests are responded to with "Service Unavailable" while Orchard is setting up.
            context.ShellSettings.State = TenantState.Initializing;

            var shellSettings = new ShellSettings(context.ShellSettings);

            shellSettings["ConnectionString"] = context.DatabaseConnectionString;

            // Creating a standalone environment based on a "minimum shell descriptor".
            // In theory this environment can be used to resolve any normal components by interface, and those
            // components will exist entirely in isolation - no crossover between the safemode container currently in effect
            // It is used to initialize the database before the recipe is run.

            var shellDescriptor = new ShellDescriptor {
                Features = context.EnabledFeatures.Select(id => new ShellFeature { Id = id }).ToList()
            };

            using (var shellContext = await _shellContextFactory.CreateDescribedContextAsync(shellSettings, shellDescriptor)) {
                using (var scope = shellContext.CreateScope()) {

                    try
                    {
                        // Migrate database
                        var dbMigrator = scope.ServiceProvider.GetRequiredService<IDbMigrator>();
                        await dbMigrator.MigrateSchemaAsync();

                        // Load seed data
                        var seedsLoader = scope.ServiceProvider.GetRequiredService<ISeedSynchronizer>();
                        await seedsLoader.SynchronizeAllSeedsAsync();

                        // Load views
                        var viewLoader = scope.ServiceProvider.GetRequiredService<IViewDataSynchronizer>();
                        await viewLoader.SynchronizeAllViewsAsync();
                    }
                    catch (Exception e) {
                        // Tables already exist or database was not found

                        // The issue is that the user creation needs the tables to be present,
                        // if the user information is not valid, the next POST will try to recreate the
                        // tables. The tables should be rolled back if one of the steps is invalid,
                        // unless the recipe is executing?

                        _logger.LogError(e, "An error occurred while initializing the datastore.");
                        context.SetError("DatabaseProvider", T["An error occurred while initializing the datastore: {0}", e.Message]);
                        return null;
                    }

                    // Create the "minimum shell descriptor"
                    await scope
                        .ServiceProvider
                        .GetService<IShellDescriptorManager>()
                        .UpdateShellDescriptorAsync(0,
                            shellContext.Blueprint.Descriptor.Features,
                            shellContext.Blueprint.Descriptor.Parameters);

                    var deferredTaskEngine = scope.ServiceProvider.GetService<IDeferredTaskEngine>();

                    if (deferredTaskEngine != null && deferredTaskEngine.HasPendingTasks) {
                        var taskContext = new DeferredTaskContext(scope.ServiceProvider);
                        await deferredTaskEngine.ExecuteTasksAsync(taskContext);
                    }
                }

                executionId = Guid.NewGuid().ToString("n");

                /*
                var recipeExecutor = shellContext.ServiceProvider.GetRequiredService<IRecipeExecutor>();

                await recipeExecutor.ExecuteAsync(executionId, context.Recipe, new
                {
                    SiteName = context.SiteName,
                    AdminUsername = context.AdminUsername,
                    AdminEmail = context.AdminEmail,
                    AdminPassword = context.AdminPassword,
                    DatabaseProvider = context.DatabaseProvider,
                    DatabaseConnectionString = context.DatabaseConnectionString,
                    DatabaseTablePrefix = context.DatabaseTablePrefix
                },

                _applicationLifetime.ApplicationStopping);
                */
            }

            // Reloading the shell context as the recipe  has probably updated its features
            using (var shellContext = await _shellHost.CreateShellContextAsync(shellSettings)) {
                using (var scope = shellContext.CreateScope()) {
                    var hasErrors = false;

                    void reportError(string key, string message) {
                        hasErrors = true;
                        context.SetError(key, message);
                    }

                    // Invoke modules to react to the setup event
                    var setupEventHandlers = scope.ServiceProvider.GetServices<ISetupEventHandler>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<SetupService>>();

                    //Notify all registered event handlers
                    await setupEventHandlers.InvokeAsync(x => x.Setup(context, reportError), logger);

                    if (hasErrors) {
                        return executionId;
                    }

                    var deferredTaskEngine = scope.ServiceProvider.GetService<IDeferredTaskEngine>();

                    if (deferredTaskEngine != null && deferredTaskEngine.HasPendingTasks) {
                        var taskContext = new DeferredTaskContext(scope.ServiceProvider);
                        await deferredTaskEngine.ExecuteTasksAsync(taskContext);
                    }
                }
            }

            // Update the shell state
            shellSettings.State = TenantState.Running;
            await _shellHost.UpdateShellSettingsAsync(shellSettings);

            return executionId;
        }
    }
}
