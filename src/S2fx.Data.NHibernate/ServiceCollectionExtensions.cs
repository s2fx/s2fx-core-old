using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using S2fx.Data.NHibernate;
using S2fx.Data.NHibernate.Mapping;
using S2fx.Data.NHibernate.Mapping.Properties;
using S2fx.Data.NHibernate.UnitOfWork;
using S2fx.Data.UnitOfWork;
using S2fx.Utility;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceCollectionExtensions {

        public static OrchardCoreBuilder AddS2fxNHibernate(this OrchardCoreBuilder builder) {
            return builder.RegisterStartup<Startup>();
        }


    }

}

