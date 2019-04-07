using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace S2fx.Xaml {

    public interface IXamlService {
        Task<T> LoadAsync<T>(Stream stream);
        Task<T> LoadAsync<T>(TextReader reader);
        Task<T> LoadAsync<T>(XmlReader reader);
    }

    public static class XamlServiceExtensions {

        public static Task<T> LoadFromStringAsync<T>(this IXamlService self, string xmlStr) {
            return self.LoadAsync<T>(new StringReader(xmlStr));
        }

        public static Task<T> LoadFromFileAsync<T>(this IXamlService self, string filePath) {
            using (var stream = File.OpenRead(filePath)) {
                return self.LoadAsync<T>(stream);
            }
        }

        public static Task<T> LoadFromEmbeddedResourceAsync<T>(this IXamlService self, Assembly assembly, string path) {
            using (var stream = assembly.GetManifestResourceStream(path)) {
                if (stream == null) {
                    throw new FileNotFoundException();
                }
                return self.LoadAsync<T>(stream);
            }
        }

    }

}
