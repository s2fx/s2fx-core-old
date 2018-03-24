using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity("Core.Sequence"), DisplayName("Sequence")]
    public class SequenceEntity : AbstractAuditedEntity {

        [Required(AllowEmptyStrings = false), MaxLength(256)]
        public virtual string Name { get; set; }

        [Required(AllowEmptyStrings = true), MaxLength(256)]
        public virtual string Prefix { get; set; } = "";

        [Required(AllowEmptyStrings = true), MaxLength(256)]
        public virtual string Suffix { get; set; } = "";

        [Required]
        public virtual long InitialValue { get; set; } = 1;

        [Required]
        public virtual long Step { get; set; } = 1;

        [Required]
        public virtual int Padding { get; set; } = 6;
    }
}
