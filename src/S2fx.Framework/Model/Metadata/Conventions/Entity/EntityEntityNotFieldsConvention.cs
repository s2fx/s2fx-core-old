using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityEntityNotFieldsConvention : AbstractEntityConvention {

        public override void Apply(MetaEntity metadata) {
            var keysToRemove = new List<string>();
            foreach (var field in metadata.Fields) {
                if (field.Value.Attributes.Where(x => x.GetType() == typeof(NotFieldAttribute)).SingleOrDefault() != null) {
                    keysToRemove.Add(field.Key);
                }
            }
            foreach (var key in keysToRemove) {
                metadata.Fields.Remove(key);
            }
        }
    }

}
