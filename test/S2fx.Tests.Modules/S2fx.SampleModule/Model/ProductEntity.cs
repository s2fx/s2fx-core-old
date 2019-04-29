using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.SampleModule.Model {

    [Entity(EntityName), DisplayName("Product")]
    public class ProductEntity : AbstractEntity {
        public const string EntityName = "Sample.Product";

        public virtual string Name { get; set; }

        public virtual decimal UnitPrice { get; set; }
    }

}
