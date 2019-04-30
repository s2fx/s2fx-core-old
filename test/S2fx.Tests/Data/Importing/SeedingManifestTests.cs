using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Model;
using S2fx.Xaml;
using S2fx.Data.Importing.Schemas;
using Xunit;

namespace S2fx.Tests.Data.Importing {

    public class SeedingManifestTests {

        [Fact]
        public async Task LoadXamlSeedingManifestFileShouldBeOk() {
            var xamlService = new PortableXamlXamlService();
            var assembly = typeof(OrganizationEntity).Assembly;
            var obj = await xamlService.LoadFromEmbeddedResourceAsync<object>(assembly, "S2fx.Core.SeedData>Init>InitSeeds.Manifest.xaml");
            Assert.NotNull(obj);
            Assert.IsType<SeedManifest>(obj);
            var sm = obj as SeedManifest;
            Assert.NotEmpty(sm.DataSources);
        }

    }

}
