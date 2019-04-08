using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using S2fx.Conventions;
using S2fx.Model;
using S2fx.Utility;
using S2fx.Model.Metadata;
using NHibernate;

namespace S2fx.Data.NHibernate.Mapping {

    public class EntityMappingClass<TEntity> : ClassMapping<TEntity>
        where TEntity : class, IEntity {

        private readonly IEntityManager _entityManager;
        private readonly Dictionary<string, Fields.IFieldMapper> _propertyMappers = new Dictionary<string, Fields.IFieldMapper>();
        protected MetaEntity MetaEntity { get; }

        public EntityMappingClass(
            IEnumerable<Fields.IFieldMapper> propertyMappers,
            IEntityManager entityManager) {
            _entityManager = entityManager;

            foreach (var pm in propertyMappers) {
                _propertyMappers.Add(pm.FieldTypeName, pm);
            }

            this.MetaEntity = entityManager.GetEntityByClrType(typeof(TEntity));

            this.DoMapping();
        }

        protected virtual void DoMapping() {
            this.MapEntityHeader();
            this.MapAllFields();
        }

        protected virtual void MapEntityHeader() {
            Table(this.MetaEntity.DbName);
        }

        protected virtual void MapAllFields() {

            var idProperty = this.MetaEntity.Fields[nameof(IEntity.Id)];
            this.Id(x => x.Id, mapper => {
                mapper.Column(idProperty.DbName);
                mapper.Generator(Generators.Native);
            });

            foreach (var field in this.MetaEntity.Fields.Values.Where(x => x.Name != nameof(IEntity.Id))) {
                if (_propertyMappers.TryGetValue(field.Type.Name, out var mapper)) {
                    mapper.MapField(this.CustomizersHolder, this.ExplicitDeclarationsHolder, this.PropertyPath, this.MetaEntity, field);
                }
                else {
                    throw new InvalidOperationException($"Unknown entity property type: '{field.Type.Name}' in entity '{this.MetaEntity.Name}'");
                }
            }
        }

    }

}
