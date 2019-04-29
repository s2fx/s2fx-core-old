using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.SampleModule.Model {

    [Entity(EntityName), DisplayName("Employee")]
    public class EmployeeEntity : AbstractEntity {
        public const string EntityName = "Sample.Employee";

        public virtual string Name { get; set; }
    }

}
