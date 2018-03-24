using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity(EntityName), DisplayName("File")]
    public class FileEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.File";

        [Required, MaxLength(1024)]
        public virtual string Name { get; set; }

        [Required, MaxLength(2048)]
        public virtual string Path { get; set; }

        [Required]
        public virtual long Size { get; set; }

        [Required, MaxLength(512)]
        public virtual byte[] HashCode { get; set; }

        [Required, MaxLength(64)]
        public virtual string Storage { get; set; } = "LocalFileSystem";

        [MaxLength(64)]
        public virtual string MimeType { get; set; }

        [Lazy]
        public virtual byte[] FileContent { get; set; }

        [NotMapped]
        public virtual string FriendlySizeText => $"{this.Size.ToString()} bytes";
    }

}
