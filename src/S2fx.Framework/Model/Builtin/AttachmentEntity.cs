using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity(EntityName), DisplayName("Attachment")]
    public class AttachmentEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.Attachment";

        [Required]
        public virtual long ObjectId { get; set; }

        [Required]
        public virtual string ObjectName { get; set; }

        [ManyToOneProperty, Required]
        public virtual FileEntity File { get; set; }

    }

}
