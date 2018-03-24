using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using S2fx.Convention;
using S2fx.Model;
using S2fx.Utility;
using S2fx.Model.Metadata;
using NHibernate;

namespace S2fx.Data.NHibernate.Mapping {

    public class EntityMappingClass<TEntity> : ClassMapping<TEntity>
        where TEntity : class, IEntity {

        private readonly IEntityManager _entityManager;
        private readonly Dictionary<string, Properties.IPropertyMapper> _propertyMappers = new Dictionary<string, Properties.IPropertyMapper>();
        protected MetaEntity MetaEntity { get; }

        public EntityMappingClass(
            IEnumerable<Properties.IPropertyMapper> propertyMappers,
            IEntityManager entityManager) {
            _entityManager = entityManager;

            foreach (var pm in propertyMappers) {
                _propertyMappers.Add(pm.PropertyTypeName, pm);
            }

            this.MetaEntity = entityManager.GetEntityByClrType(typeof(TEntity));

            this.DoMapping();
        }

        protected void DoMapping() {
            this.MapEntityHeader();
            this.MapEntityProperties();
        }

        protected void MapEntityHeader() {
            Table(this.MetaEntity.DbName);
        }

        protected void MapEntityProperties() {

            var idProperty = this.MetaEntity.Properties["Id"];
            this.Id(x => x.Id, mapper => {
                mapper.Column(idProperty.DbName);
                mapper.Generator(Generators.Native);
            });

            foreach (var property in this.MetaEntity.Properties.Values.Where(x => x.Name != "Id")) {
                var mapper = _propertyMappers[property.Type.Name];
                mapper.MapProperty(this.CustomizersHolder, this.ExplicitDeclarationsHolder, this.PropertyPath, this.MetaEntity, property);
            }
        }

    }

}
