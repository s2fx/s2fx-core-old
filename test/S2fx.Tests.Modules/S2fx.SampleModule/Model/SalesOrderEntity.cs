using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.SampleModule.Model {

    [Entity(EntityName), DisplayName("Sales Order")]
    public class SalesOrderEntity : AbstractEntity {
        public const string EntityName = "Sample.SalesOrder";

        [ManyToOneField]
        public virtual PartnerrEntity Customer { get; set; }

        [ManyToOneField]
        public virtual EmployeeEntity Salesman { get; set; }

        [OneToManyField(nameof(SalesOrderLineEntity.Order))]
        public virtual ICollection<SalesOrderLineEntity> Lines { get; set; }

    }

    [Entity(EntityName), DisplayName("Sales Order Line")]
    public class SalesOrderLineEntity : AbstractEntity {
        public const string EntityName = "Sample.SalesOrderLine";

        public SalesOrderLineEntity() {
        }

        [ManyToOneField]
        public virtual ProductEntity Product { get; set; }

        public virtual decimal UnitPrice { get; set; }

        public virtual decimal Quantity { get; set; }

        [ManyToOneField]
        public virtual SalesOrderEntity Order { get; set; }
    }

}
