using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.View.Model.Model {

    [Entity(EntityName), DisplayName("View")]
    public class ViewEntity : AbstractDefinitionEntity {
        public const string EntityName = "Core.View";

        [MaxLength(256)]
        public virtual string Entity { get; set; }

        public virtual int Priority { get; set; }

        public virtual string ViewType { get; set; }
    }

}
