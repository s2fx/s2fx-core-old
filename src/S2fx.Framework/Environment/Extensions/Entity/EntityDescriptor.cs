using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S2fx.Model.Metadata.Types;

namespace S2fx.Environment.Extensions.Entity {

    public class EntityDescriptor {
        public string Module { get; }
        public string Name { get; }
        public IEntityType Type { get; }
        public string[] Dependencies { get; }
        public Type ClrType { get; }

        public EntityDescriptor(string module, string name, IEntityType type, IEnumerable<string> dependencies, Type clrType = null) {
            this.Module = module;
            this.Name = name;
            this.Type = type;
            this.Dependencies = dependencies.ToArray();
            this.ClrType = clrType;
        }
    }

}
