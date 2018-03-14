using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.Metadata.Model {

    [Table("meta_entity")]
    public class MetaEntity : AbstractEntity {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

}
