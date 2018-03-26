using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing.Model {

    public class DataSourceInfo {

        public string Kind { get; set; }

        public IReadOnlyDictionary<string, object> Properties { get; set; } = 
            new Dictionary<string, object>();
    }

}
