using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Modules;
using S2fx.Data.Importing;
using S2fx.View;

namespace S2fx.Modules {

    public interface IS2Startup : IStartup {
        void ConfigureSeeds(IFileDataSourceCollection initSeeds, IFileDataSourceCollection demoSeeds);
        void ConfigureViews(IViewDefinitionsCollection views);
    }

}
