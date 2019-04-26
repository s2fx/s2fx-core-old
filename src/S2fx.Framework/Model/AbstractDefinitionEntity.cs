using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model {

    public abstract class AbstractDefinitionEntity : AbstractEntity {

        [Required, MaxLength(255)]
        public virtual string Name { get; set; }

        [Required, MaxLength(255)]
        public virtual string Feature { get; set; }

        [MaxLength(256)]
        public virtual string DisplayName { get; set; }


        [Required, MultiLine, Lazy]
        public virtual string Definition { get; set; }

    }

}
