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

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Path { get; set; }

        [Required]
        public virtual long Size { get; set; }

        [Required]
        public virtual byte[] HashCode { get; set; }

        [Required]
        public virtual string Storage { get; set; } = "LocalFileSystem";

        public virtual string MimeType { get; set; }

        [Lazy]
        public virtual byte[] FileContent { get; set; }

        [NotMapped]
        public string FriendlySizeText => $"{this.Size.ToString()} bytes";
    }

}
