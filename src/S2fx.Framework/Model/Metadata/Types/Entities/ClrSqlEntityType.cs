using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model.Metadata.Loaders;

namespace S2fx.Model.Metadata.Types {


    public interface IClrSqlEntityType : IEntityType {

    }

    public class ClrSqlEntityType : IClrSqlEntityType {

        public string Name => BuiltinEntityTypeNames.ClrSqlEntityTypeName;

        private readonly IServiceProvider _services;

        public ClrSqlEntityType(IServiceProvider services) {
            _services = services;
        }

        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        public async Task<MetaEntity> LoadAsync(EntityInfo descriptor) {
            var loader = _services.GetRequiredService<IClrTypeEntityMetadataLoader>();
            var entity = loader.LoadEntityByClr(descriptor.ClrType);
            await Task.CompletedTask;
            return entity;
        }
    }

}
