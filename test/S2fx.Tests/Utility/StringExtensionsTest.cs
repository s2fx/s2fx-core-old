using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using S2fx.Utility;

namespace S2fx.Tests.Utility {

    public class StringExtensionsTest {

        [Fact]
        public void ToSnakeCaseShouldBeOK() {
            Assert.Equal("hasta_la_vista_baby", "HastaLaVistaBaby".ToSnakeCase());
            Assert.Equal("_hasta_la_vista_baby", "_HastaLaVistaBaby".ToSnakeCase());
            Assert.Equal("_hasta_la_vista_baby", "_Hasta_LaVistaBaby".ToSnakeCase());
        }
    }

}
