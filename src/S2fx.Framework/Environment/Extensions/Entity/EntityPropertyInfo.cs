using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.Environment.Extensions.Entity {

    public class EntityPropertyInfo {
        public EntityInfo EntityInfo { get; set; }
        public string Name { get; set; }
        public IEnumerable<Attribute> Attributes { get; set; }
        public PropertyInfo ClrPropertyInfo { get; set; }
    }

}
