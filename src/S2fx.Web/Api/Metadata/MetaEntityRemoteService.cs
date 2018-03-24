using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using S2fx.Remoting;

namespace S2fx.Web.Api.Metadata {

    [RemoteService(Name = "MetaEntity")]
    public class MetaEntityRemoteService {

        public MetaEntityRemoteService() {

        }

        public string Index() {
            return "Hello from MetaEntity";
        }
    }

}
