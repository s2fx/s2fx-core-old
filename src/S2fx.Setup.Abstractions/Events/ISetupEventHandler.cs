using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Setup.Events {

    /// <summary>
    /// Called when a tenant is set up.
    /// </summary>
    public interface ISetupEventHandler {
        Task Setup(ISetupInfo setupInfo, Action<string, string> reportError);
    }

}
