using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Xaml {

    public interface IXamlService {
        Task<object> LoadAsync(Stream stream);
    }

}
