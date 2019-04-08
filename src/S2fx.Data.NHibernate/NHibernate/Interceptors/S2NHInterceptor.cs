using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Type;
using S2fx.Model;
using S2fx.Services;

namespace S2fx.Data.NHibernate.Interceptors {

    /// <summary>
    /// NHibernate 的拦截器，系统只有一个作为单体存在
    /// </summary>
    public class S2NHInterceptor : EmptyInterceptor {

        readonly IClock _clock;

        public S2NHInterceptor(IClock clock) {
            _clock = clock;
        }

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
                        currentState[i] = _clock.UtcNow();
                        break;
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
                        state[i] = _clock.UtcNow();
                        break;
                    }
                }
            }
            return false;
        }

    }

}
