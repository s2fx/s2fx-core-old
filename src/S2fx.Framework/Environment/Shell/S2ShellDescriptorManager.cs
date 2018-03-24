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

namespace S2fx.Environment.Shell {

    /// <summary>
    /// Implements <see cref="IShellDescriptorManager"/> by providing the list of features store in the database. 
    /// </summary>
    public class S2ShellDescriptorManager : IShellDescriptorManager {
        private readonly ShellSettings _shellSettings;
        private readonly IEnumerable<IShellDescriptorManagerEventHandler> _shellDescriptorManagerEventHandlers;
        private readonly ILogger _logger;
        private ShellDescriptor _shellDescriptor;

        public S2ShellDescriptorManager(
            ShellSettings shellSettings,
            IEnumerable<IShellDescriptorManagerEventHandler> shellDescriptorManagerEventHandlers,
            ILogger<S2ShellDescriptorManager> logger) {
            _shellSettings = shellSettings;
            _shellDescriptorManagerEventHandlers = shellDescriptorManagerEventHandlers;
            _logger = logger;
        }

        public async Task<ShellDescriptor> GetShellDescriptorAsync() {
            // Prevent multiple queries during the same request
            if (_shellDescriptor == null) {
                //_shellDescriptor = await _session.Query<ShellDescriptor>().FirstOrDefaultAsync();
            }

            return _shellDescriptor;
        }

        public async Task UpdateShellDescriptorAsync(int priorSerialNumber, IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters) {
            var shellDescriptorRecord = await GetShellDescriptorAsync();
            var serialNumber = shellDescriptorRecord == null ? 0 : shellDescriptorRecord.SerialNumber;
            if (priorSerialNumber != serialNumber) {
                throw new InvalidOperationException("Invalid serial number for shell descriptor");
            }

            if (_logger.IsEnabled(LogLevel.Information)) {
                _logger.LogInformation("Updating shell descriptor for shell '{0}'...", _shellSettings.Name);
            }

            if (shellDescriptorRecord == null) {
                shellDescriptorRecord = new ShellDescriptor { SerialNumber = 1 };
            }
            else {
                shellDescriptorRecord.SerialNumber++;
            }

            shellDescriptorRecord.Features = enabledFeatures.ToList();
            shellDescriptorRecord.Parameters = parameters.ToList();

            if (_logger.IsEnabled(LogLevel.Information)) {
                _logger.LogInformation("Shell descriptor updated for shell '{0}'.", _shellSettings.Name);
            }

            //_session.Save(shellDescriptorRecord);

            // Update cached reference
            _shellDescriptor = shellDescriptorRecord;

            await _shellDescriptorManagerEventHandlers.InvokeAsync(e => e.Changed(shellDescriptorRecord, _shellSettings.Name), _logger);
        }
    }
}
