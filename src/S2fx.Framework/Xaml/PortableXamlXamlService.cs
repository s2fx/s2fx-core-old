using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Portable.Xaml;

namespace S2fx.Xaml {

    public class PortableXamlXamlService : IXamlService {

        private readonly XamlXmlReaderSettings _readerSettings;

        public PortableXamlXamlService() {
            _readerSettings = new XamlXmlReaderSettings() {
                LocalAssembly = this.GetType().Assembly,
            };
            _readerSettings.AddNamespaces(this.GetType().Assembly);
            _readerSettings.AddNamespaces(typeof(View.Schemas.S2ViewDefinitions).Assembly);
        }

        public Task<T> LoadAsync<T>(TextReader reader) {
            using (var xamlReader = new XamlXmlReader(reader, _readerSettings)) {
                return this.InternalLoadAsync<T>(xamlReader);
            }
        }

        public Task<T> LoadAsync<T>(Stream stream) {
            using (var xamlReader = new XamlXmlReader(stream, _readerSettings)) {
                return this.InternalLoadAsync<T>(xamlReader);
            }
        }

        public Task<T> LoadAsync<T>(XmlReader reader) {
            using (var xamlReader = new XamlXmlReader(reader, _readerSettings)) {
                return this.InternalLoadAsync<T>(xamlReader);
            }
        }

        public string Save(object instance) {
            return XamlServices.Save(instance);
        }

        private Task<T> InternalLoadAsync<T>(XamlXmlReader xamlReader) {
            using (var writer = new XamlObjectWriter(xamlReader.SchemaContext)) {
                XamlServices.Transform(xamlReader, writer, false);
                var result = (T)writer.Result;
                return Task.FromResult(result);
            }
        }

        private Task<string> InternalSaveAsync(XamlObjectReader objectReader) {
            var sctx = new XamlSchemaContext(new Assembly[] { this.GetType().Assembly }, null);
            using (var sw = new StringWriter())
            using (var writer = new XamlXmlWriter(sw, sctx)) {
                XamlServices.Transform(objectReader, writer, false);
                sw.Flush();
                return Task.FromResult(sw.ToString());
            }
        }

    }

}
