using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.View.Model.Model {

    [Entity(EntityName), DisplayName("View Fragment")]
    public class ViewFragmentEntity : AbstractDefinitionEntity {
        public const string EntityName = "Core.ViewFragment";

        [ManyToOneField, Required]
        public virtual ViewEntity Extends { get; set; }

        public virtual int Priority { get; set; }

    }

}
