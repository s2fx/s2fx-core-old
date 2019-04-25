using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;
using S2fx.Data.Importing.Model;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing {

    public class ImportingTaskContext {

        public IServiceProvider ServiceProvider { get; }
        public IFileProvider FileProvider { get; }

        public ImportingTaskContext(IServiceProvider sp, IFileProvider fileProvider) {
            this.ServiceProvider = sp ?? throw new ArgumentNullException(nameof(sp));
            this.FileProvider = fileProvider;
        }
    }
}
