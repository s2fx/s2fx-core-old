using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Metadata {

    public class EntityPropertyInfo : AbstractMetadata {

        public EntityAttribute GetEntityAttribute() =>
            this.Attributes.Single(x => x is EntityAttribute) as EntityAttribute;

        public TableAttribute GetTableAttribute() =>
            this.Attributes.SingleOrDefault(x => x is TableAttribute) as TableAttribute;
    }

}
