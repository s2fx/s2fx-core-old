using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Model;
using S2fx.Xaml;
using S2fx.View.Schemas;
using Xunit;

namespace S2fx.Tests.View.Data {

    public class ViewsManifestTests {

        [Fact]
        public async Task LoadXamlViewsManifestFileShouldBeOk() {
            var xamlService = new PortableXamlXamlService();
            var assembly = typeof(OrganizationEntity).Assembly;
            var obj = await xamlService.LoadFromEmbeddedResourceAsync<object>(assembly, "S2fx.Core.ViewsManifest.xaml");
            Assert.NotNull(obj);
            Assert.IsType<ViewsManifest>(obj);
            var sm = obj as ViewsManifest;
            Assert.NotEmpty(sm.ViewFiles);
        }

    }

}
