using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.SampleModule.Model {

    [Entity(EntityName), DisplayName("Business Partner")]
    public class PartnerrEntity : AbstractEntity {
        public const string EntityName = "Sample.Partner";

        public virtual string Name { get; set; }
        public virtual bool IsCustomer { get; set; }
        public virtual bool IsSupplier { get; set; }
    }

}
