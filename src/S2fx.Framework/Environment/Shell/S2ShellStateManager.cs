using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.State;

namespace S2fx.Environment.Shell {

    /// <summary>
    /// Stores <see cref="ShellState"/> in the database. 
    /// </summary>
    /*
    public class S2ShellStateManager : IShellStateManager {
        private ShellState _shellState;

        public S2ShellStateManager(ILogger<S2ShellStateManager> logger) {
            Logger = logger;
        }

        ILogger Logger { get; }

        public Task<ShellState> GetShellStateAsync() {
            if (_shellState != null) {
                return Task.FromResult(_shellState);
            }

            //_shellState = await _session.Query<ShellState>().FirstOrDefaultAsync();

            if (_shellState == null) {
                _shellState = new ShellState();
                UpdateShellState();
            }

            return Task.FromResult(_shellState);
        }

        public async Task UpdateEnabledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value) {
            if (Logger.IsEnabled(LogLevel.Debug)) {
                Logger.LogDebug("Feature {0} EnableState changed from {1} to {2}",
                             featureState.Id, featureState.EnableState, value);
            }

            var previousFeatureState = await this.GetOrCreateFeatureStateAsync(featureState.Id);
            if (previousFeatureState.EnableState != featureState.EnableState) {
                if (Logger.IsEnabled(LogLevel.Warning)) {
                    Logger.LogWarning("Feature {0} prior EnableState was {1} when {2} was expected",
                               featureState.Id, previousFeatureState.EnableState, featureState.EnableState);
                }
            }

            previousFeatureState.EnableState = value;
            featureState.EnableState = value;

            UpdateShellState();
        }

        public async Task UpdateInstalledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value) {
            if (Logger.IsEnabled(LogLevel.Debug)) {
                Logger.LogDebug("Feature {0} InstallState changed from {1} to {2}", featureState.Id, featureState.InstallState, value);
            }

            var previousFeatureState = await this.GetOrCreateFeatureStateAsync(featureState.Id);
            if (previousFeatureState.InstallState != featureState.InstallState) {
                if (Logger.IsEnabled(LogLevel.Warning)) {
                    Logger.LogWarning("Feature {0} prior InstallState was {1} when {2} was expected",
                               featureState.Id, previousFeatureState.InstallState, featureState.InstallState);
                }
            }

            previousFeatureState.InstallState = value;
            featureState.InstallState = value;

            UpdateShellState();
        }

        private async Task<ShellFeatureState> GetOrCreateFeatureStateAsync(string id) {
            var shellState = await this.GetShellStateAsync();
            var featureState = shellState.Features.FirstOrDefault(x => x.Id == id);

            if (featureState == null) {
                featureState = new ShellFeatureState() { Id = id };
                _shellState.Features.Add(featureState);
            }

            return featureState;
        }

        private void UpdateShellState() {
            //_session.Save(_shellState);
        }
    }
    */

}
