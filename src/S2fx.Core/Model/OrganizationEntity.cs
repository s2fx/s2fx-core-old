using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model {

    [Entity(EntityName), DisplayName("Organization")]
    public class OrganizationEntity : AbstractHierarchyEntity<OrganizationEntity> {
        public const string EntityName = "Core.Organization";

        public virtual string Name { get; set; }

    }

}
