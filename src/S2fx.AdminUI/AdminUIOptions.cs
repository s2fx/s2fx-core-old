
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace S2fx.AdminUI {

    public class AdminUIOptions {
        public IList<IFileProvider> FileProviders { get; } = new List<IFileProvider>();
    }

}
