using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Type;
using S2fx.Model;

namespace S2fx.Data.NHibernate.Interceptors {

    public class AuditInterceptor : EmptyInterceptor {

        public override void OnDelete(object entity, object id, object[] state,
            string[] propertyNames, IType[] types) {
            // do nothing
        }

        public override bool OnFlushDirty(
            object entity, object id, object[] currentState, object[] previousState,
            string[] propertyNames, IType[] types) {

            if (entity is IAuditedEntity) {
                for (int i = 0; i < propertyNames.Length; i++) {
                    if (nameof(IAuditedEntity.UpdatedOn).Equals(propertyNames[i])) {
                        currentState[i] = DateTime.UtcNow;
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool OnSave(object entity, object id, object[] state,
            string[] propertyNames, IType[] types) {

            if (entity is IAuditedEntity) {
                for (int i = 0; i < propertyNames.Length; i++) {
                    if (nameof(IAuditedEntity.CreatedOn).Equals(propertyNames[i])) {
                        state[i] = DateTime.UtcNow;
                        return true;
                    }
                }
            }
            return false;
        }

    }

}
