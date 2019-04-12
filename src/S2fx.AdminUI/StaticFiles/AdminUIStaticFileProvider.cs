using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace S2fx.AdminUI.StaticFiles {

    public class AdminUIStaticFileProvider : IFileProvider {
        readonly IFileProvider _staticFileProvider;

        public AdminUIStaticFileProvider(IServiceProvider services) {
            var options = services.GetRequiredService<IOptions<StaticFileOptions>>().Value;
            _staticFileProvider = options.FileProvider;
        }

        public IDirectoryContents GetDirectoryContents(string subpath) =>
            _staticFileProvider.GetDirectoryContents(GetModuleSubpath(subpath));


        public IFileInfo GetFileInfo(string subpath) =>
            _staticFileProvider.GetFileInfo(GetModuleSubpath(subpath));


        public IChangeToken Watch(string filter) =>
            NullChangeToken.Singleton;

        static string GetModuleSubpath(string subpath) {
            var x = $"/{AdminUIConstants.AdminUIModuleId}{subpath}";
            return x;
        }

    }

}
