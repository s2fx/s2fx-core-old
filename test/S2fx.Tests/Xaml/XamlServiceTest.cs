using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Model;
using S2fx.View.Schemas;
using S2fx.Xaml;
using Xunit;

namespace S2fx.Tests.Xaml {

    public class XamlServiceTest {

        [Fact]
        public async Task LoadXamlFromEmbeddedResourceShouldBeOk() {
            var xamlService = new PortableXamlXamlService();
            var assembly = typeof(OrganizationEntity).Assembly;
            var obj = await xamlService.LoadFromEmbeddedResourceAsync<object>(assembly, "S2fx.Core.S2Views>UserViews.xaml");
            Assert.NotNull(obj);
            Assert.IsType<S2ViewDefinition>(obj);
        }

    }

}
