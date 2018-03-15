using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Model.Entities;

namespace S2fx.Model {

    [Entity, Table("core_organization"), DisplayName("Organization")]
    public class OrganizationEntity : AbstractAuditedEntity {

        public string Name { get; set; }

        public OrganizationEntity Parent { get; set; }

    }

}
