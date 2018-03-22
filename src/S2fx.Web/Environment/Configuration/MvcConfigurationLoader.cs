using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using S2fx.Environment.Configuration;

namespace S2fx.Web.Environment.Configuration {

    public class MvcConfigurationLoader : IConfigurationLoader {
        private readonly IConfiguration _cfg;

        public MvcConfigurationLoader(IConfiguration cfg) {
            _cfg = cfg;
        }

        public S2Settings GetSettings() =>
            _cfg.GetSection(WellKnownConstants.SlipstreamConfigurationSection).Get<S2Settings>();


    }

}
