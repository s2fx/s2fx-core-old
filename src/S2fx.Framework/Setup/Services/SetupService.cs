using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Setup.Model;

namespace S2fx.Setup.Services {

    public interface ISetupService {
        Task SetupAsync(SetupContext context);
    }


    public class SetupService : ISetupService {

        public async Task SetupAsync(SetupContext context) {
            await Task.CompletedTask;
        }
    }
}
