using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Modules;
using S2fx.Data.Importing.Seeds;
using S2fx.View.Data;

namespace S2fx.Data {

    public class AutoDBMigrator : ModularTenantEvents {
        private readonly ILogger<AutoDBMigrator> _logger;
        private readonly IServiceProvider _services;

        public AutoDBMigrator(ILogger<AutoDBMigrator> logger, IServiceProvider sp) {
            _logger = logger;
            _services = sp;
        }

        public override async Task ActivatingAsync() {

            var dbMigrator = _services.GetRequiredService<IDbMigrator>();
            await dbMigrator.MigrateSchemaAsync();

            var seedsLoader = _services.GetRequiredService<ISeedSynchronizer>();
            await seedsLoader.SynchronizeAllSeedsAsync();
            var viewLoader = _services.GetRequiredService<IViewDataSynchronizer>();
            await viewLoader.SynchronizeAllViewsAsync();
        }

    }

}
