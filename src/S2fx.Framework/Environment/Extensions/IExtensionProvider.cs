using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.Modules;
using S2fx.Model;

namespace S2fx.Environment.Extensions {

    public interface IExtensionProvider {
        IEnumerable<Module> GetAllModules();
        IEnumerable<Type> GetAllRelationEntityTypes();
    }

    public class ExtensionProvider : IExtensionProvider {
        private readonly IHostingEnvironment _hostEnv;

        public ExtensionProvider(IHostingEnvironment env) {
            _hostEnv = env;
        }

        public IEnumerable<Module> GetAllModules() =>
            _hostEnv.GetApplication().ModuleNames.Select(n => _hostEnv.GetModule(n));

        public IEnumerable<Type> GetAllRelationEntityTypes() {
            var modules = this.GetAllModules();
            var types = modules.AsParallel()
                .WithDegreeOfParallelism(8)
                .SelectMany(m => m.GetEntityTypes())
                .ToArray();
            return types;
        }
    }
}
