using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Portable.Xaml;

namespace S2fx.Xaml {

    public class PortableXamlXamlService : IXamlService {

        public Task<object> LoadAsync(Stream stream) {

            var settings = new XamlXmlReaderSettings() {
                LocalAssembly = this.GetType().Assembly,
            };
            settings.AddNamespaces(this.GetType().Assembly);
            var reader = new XamlXmlReader(stream, settings);
            using (var writer = new XamlObjectWriter(reader.SchemaContext)) {
                XamlServices.Transform(reader, writer, false);
                return Task.FromResult(writer.Result);
            }
        }

    }

}
