using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Modules;
using S2fx.Data.Importing;
using S2fx.View;

namespace S2fx.Modules {

    public abstract class S2ModuleStartupBase : StartupBase, IS2Startup {

        public virtual void ConfigureViews(IViewDefinitionsCollection views) {
        }

        public virtual void ConfigureSeeds(ISeedManifestCollection initSeeds, ISeedManifestCollection demoSeeds) {
        }
    }

}
