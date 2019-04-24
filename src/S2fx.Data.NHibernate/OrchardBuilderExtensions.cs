using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using S2fx.Remoting;
using S2fx;
using S2fx.Environment.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;

namespace S2fx.Data.NHibernate {

    public static class OrchardBuilderExtensions {

        public static OrchardCoreBuilder AddS2fxNHibernate(this OrchardCoreBuilder builder) {
            return builder
                .ConfigureServices(s => s.WithNHibernate());
        }

    }

}
