using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using S2fx.Data;
using S2fx.Data.Importing.Seeds;
using S2fx.Model;
using S2fx.Remoting;
using S2fx.View.Data;

namespace S2fx.Setup.RemoteServices {

    public class UpdateFeaturesDataOptions {
        public string Feature { get; set; }
    }

    [RemoteService(name: "Feature", Area = MvcControllerAreas.SystemArea)]
    public class FeatureRemoteService {
        readonly IDbMigrator _dbMigrator;
        readonly ISeedSynchronizer _seedSynchronizer;
        readonly IViewDataSynchronizer _viewDataSynchronizer;

        public ILogger Logger { get; }

        public FeatureRemoteService(
            IDbMigrator dbMigrator,
            ISeedSynchronizer seedSynchronizer,
            IViewDataSynchronizer viewDataSynchronizer,
            ILogger<FeatureRemoteService> logger) {

            _dbMigrator = dbMigrator;
            _seedSynchronizer = seedSynchronizer;
            _viewDataSynchronizer = viewDataSynchronizer;
            this.Logger = logger;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Post)]
        public async Task UpdateFeaturesDataAsync([Body]UpdateFeaturesDataOptions options) {
            await _dbMigrator.MigrateSchemaAsync();
            await _viewDataSynchronizer.SynchronizeViewsAsync(options.Feature);
            await _seedSynchronizer.SynchronizeSeedAsync(options.Feature);
        }
    }
}