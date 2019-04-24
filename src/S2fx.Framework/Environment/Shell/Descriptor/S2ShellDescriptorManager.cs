using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrchardCore.Modules;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Environment.Shell.Descriptor;
using System.Linq;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Shell.Configuration;
using Microsoft.Extensions.Configuration;
using OrchardCore.Hosting.ShellBuilders;

namespace S2fx.Environment.Shell.Descriptor {

    public class S2ShellDescriptorManager : IShellDescriptorManager {
        readonly IServiceProvider _serviceProvider;
        readonly ShellSettings _shellSettings;
        readonly IShellConfiguration _shellConfiguration;
        readonly IEnumerable<ShellFeature> _alwaysEnabledFeatures;
        readonly IEnumerable<IShellDescriptorManagerEventHandler> _shellDescriptorManagerEventHandlers;
        ShellDescriptor _shellDescriptor;

        public ILogger Logger { get; }

        public S2ShellDescriptorManager(
            IServiceProvider serviceProvider,
            ShellSettings shellSettings,
            IShellConfiguration shellConfiguration,
            IEnumerable<ShellFeature> shellFeatures,
            IEnumerable<IShellDescriptorManagerEventHandler> shellDescriptorManagerEventHandlers,
            ILogger<S2ShellDescriptorManager> logger) {
            _serviceProvider = serviceProvider;
            _shellSettings = shellSettings;
            _shellConfiguration = shellConfiguration;
            _alwaysEnabledFeatures = shellFeatures.Where(f => f.AlwaysEnabled).ToArray();
            _shellDescriptorManagerEventHandlers = shellDescriptorManagerEventHandlers;
            this.Logger = logger;
        }

        public Task<ShellDescriptor> GetShellDescriptorAsync() {
            // Prevent multiple queries during the same request
            if (_shellDescriptor == null) {
                var configuredFeatures = new ConfiguredFeatures();
                _shellConfiguration.Bind(configuredFeatures);

                var features = _alwaysEnabledFeatures.Concat(configuredFeatures.Features
                    .Select(id => new ShellFeature(id) { AlwaysEnabled = true })).Distinct();

                _shellDescriptor = new ShellDescriptor {
                    Features = features.ToList()
                };
            }

            return Task.FromResult(_shellDescriptor);
        }

        public async Task UpdateShellDescriptorAsync(int priorSerialNumber, IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters) {
            var shellDescriptorRecord = await GetShellDescriptorAsync();
            var serialNumber = shellDescriptorRecord == null
                ? 0
                : shellDescriptorRecord.SerialNumber;

            if (priorSerialNumber != serialNumber) {
                throw new InvalidOperationException("Invalid serial number for shell descriptor");
            }

            if (this.Logger.IsEnabled(LogLevel.Information)) {
                this.Logger.LogInformation("Updating shell descriptor for tenant '{TenantName}' ...", _shellSettings.Name);
            }

            if (shellDescriptorRecord == null) {
                shellDescriptorRecord = new ShellDescriptor { SerialNumber = 1 };
            }
            else {
                shellDescriptorRecord.SerialNumber++;
            }

            shellDescriptorRecord.Features = _alwaysEnabledFeatures.Concat(enabledFeatures).Distinct().ToList();
            shellDescriptorRecord.Parameters = parameters.ToList();

            if (this.Logger.IsEnabled(LogLevel.Information)) {
                this.Logger.LogInformation("Shell descriptor updated for tenant '{TenantName}'.", _shellSettings.Name);
            }

            //_session.Save(shellDescriptorRecord);

            // Update cached reference
            _shellDescriptor = shellDescriptorRecord;

            await _shellDescriptorManagerEventHandlers.InvokeAsync(e => e.Changed(shellDescriptorRecord, _shellSettings.Name), this.Logger);
        }

        private class ConfiguredFeatures {
            public string[] Features { get; set; } = Array.Empty<string>();
        }


    }

}
