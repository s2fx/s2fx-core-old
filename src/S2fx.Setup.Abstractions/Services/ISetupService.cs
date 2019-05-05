using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Setup.Services {

    public interface ISetupService {
        Task<string> SetupAsync(SetupContext context);
    }

}
