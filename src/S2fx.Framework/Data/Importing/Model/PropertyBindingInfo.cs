using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace S2fx.Data.Importing.Model {


    public class PropertyBindingInfo {

        [JsonProperty("from")]
        public string FromExpression { get; set; }

        [JsonProperty("property")]
        public string TargetProperty { get; set; }
    }

}
