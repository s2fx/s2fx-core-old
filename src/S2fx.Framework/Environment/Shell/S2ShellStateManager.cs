using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.State;

namespace S2fx.Environment.Shell {

    public class S2ShellStateManager : IShellStateManager {

        public Task<ShellState> GetShellStateAsync() {
            throw new NotImplementedException();
        }

        public Task UpdateEnabledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value) {
            throw new NotImplementedException();
        }

        public Task UpdateInstalledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value) {
            throw new NotImplementedException();
        }
    }

}
