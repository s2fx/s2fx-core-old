using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model {

    [Entity(EntityName), DisplayName("Organization")]
    public class OrganizationEntity : AbstractAuditedEntity, IHierarchyEntity<OrganizationEntity> {
        public const string EntityName = "Core.Organization";

        public string Name { get; set; }

        [ManyToOneProperty(EntityName)]
        public OrganizationEntity Parent { get; set; }

        public long RangeLeft { get; set; }

        public long RangeRight { get; set; }
    }

}
