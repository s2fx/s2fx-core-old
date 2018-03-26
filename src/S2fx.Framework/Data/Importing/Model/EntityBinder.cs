using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing.Model {

    public class EntityBinder {
        public string Entity { get; set; }
        public string Where { get; set; }
        public IEnumerable<PropertyBinder> Properties { get; set; }
    }

}
