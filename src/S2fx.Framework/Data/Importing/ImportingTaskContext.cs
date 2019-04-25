using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Data.Importing.Model;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing {

    public class ImportingTaskContext {

        public IServiceProvider ServiceProvider { get; }

        public ImportingTaskContext(IServiceProvider sp) {
            this.ServiceProvider = sp ?? throw new ArgumentNullException(nameof(sp));
        }
    }
}
