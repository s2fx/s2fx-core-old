using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.SampleModule.Model {

    [Entity(EntityName), DisplayName("Department")]
    public class DepartmentEntity : AbstractHierarchyEntity<DepartmentEntity> {
        public const string EntityName = "Sample.Department";

        public virtual string Name { get; set; }

        [ManyToManyField(mappedBy: nameof(EmployeeEntity.Departments), joinTable: "sample_department_employee")]
        public virtual ICollection<EmployeeEntity> Employees { get; set; }
    }

}
