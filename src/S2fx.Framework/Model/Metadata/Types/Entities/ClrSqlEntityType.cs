using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model.Metadata.Loaders;

namespace S2fx.Model.Metadata.Types {


    public interface IClrSqlEntityType : IEntityType {

    }

    public class ClrSqlEntityType : IClrSqlEntityType {

        public string Name => BuiltinEntityTypeNames.ClrSqlEntityTypeName;

        public IClrTypeEntityMetadataLoader _loader;

        public ClrSqlEntityType(IClrTypeEntityMetadataLoader loader) {
            _loader = loader;
        }

        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        public async Task<MetaEntity> LoadAsync(EntityDescriptor descriptor) {
            var entity = _loader.LoadEntityByClr(descriptor.ClrType);
            await Task.CompletedTask;
            return entity;
        }
    }

}
