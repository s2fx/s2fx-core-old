using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Xaml {

    public static class XamlServiceExtensions {

        public static async Task<T> LoadAsync<T>(this IXamlService self, Stream stream) =>
            (T)(await self.LoadAsync(stream));

    }

}
