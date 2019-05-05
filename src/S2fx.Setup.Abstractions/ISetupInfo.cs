using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Environment.Shell;

namespace S2fx.Setup {

    public interface ISetupInfo {
        string RootUserName { get; }
        string RootPassword { get; }
        string DbName { get; }
        string DatabaseConnectionString { get; }
        bool IsDemo { get; }
        IEnumerable<string> EnabledFeatures { get; }
    }

}
