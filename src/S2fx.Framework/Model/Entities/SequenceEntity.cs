using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Entities {

    [Entity("Core.Sequence"), DisplayName("Sequence")]
    public class SequenceEntity : AbstractAuditedEntity {

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Prefix { get; set; } = "";

        [Required(AllowEmptyStrings = true)]
        public string Suffix { get; set; } = "";

        [Required]
        public long InitialValue { get; set; } = 1;

        [Required]
        public long Step { get; set; } = 1;

        [Required]
        public int Padding { get; set; } = 6;
    }
}
