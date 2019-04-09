using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Model.Metadata;

namespace S2fx.Model.Builtin {

    [Entity("Core.Module"), DisplayName("Module")]
    public class ModuleEntity : AbstractEntity {

        [Required, MaxLength(256)]
        public virtual string Name { get; set; }

        [MaxLength(256)]
        public virtual string DisplayName { get; set; }

        [MaxLength(32)]
        public virtual string Version { get; set; }

        [OneToManyField(nameof(FeatureEntity.Module))]
        public virtual ICollection<FeatureEntity> Features { get; set; }

    }

}
