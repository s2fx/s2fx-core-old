using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace S2fx.AdminUI.StaticFiles {

    public class AdminUISpaStaticFileProvider : ISpaStaticFileProvider {

        readonly IFileProvider _provider;

        public AdminUISpaStaticFileProvider(IServiceProvider sp) {
            _provider = new AdminUIStaticFileProvider(sp);

        }

        public IFileProvider FileProvider => _provider;
    }

}
