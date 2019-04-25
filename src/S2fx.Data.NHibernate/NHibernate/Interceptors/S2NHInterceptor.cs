using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Type;
using OrchardCore.Modules;
using S2fx.Model;

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

            if (entity is IAuditedEntity auditedEntity) {
                for (int i = 0; i < propertyNames.Length; i++) {
                    if (nameof(auditedEntity._UpdatedOn).Equals(propertyNames[i])) {
                        if (auditedEntity._UpdatedOn == null || auditedEntity._UpdatedOn.Value == DateTime.MinValue) {
                            var utcNow = _clock.UtcNow;
                            currentState[i] = utcNow;
                            auditedEntity._UpdatedOn = utcNow;
                        }
                        break;
                    }
                }
            }
            return false;
        }

        public override bool OnSave(object entity, object id, object[] state,
            string[] propertyNames, IType[] types) {

            if (entity is IAuditedEntity auditedEntity) {
                for (int i = 0; i < propertyNames.Length; i++) {
                    if (nameof(IAuditedEntity._CreatedOn).Equals(propertyNames[i])) {
                        if (auditedEntity._CreatedOn == DateTime.MinValue) {
                            var utcNow = _clock.UtcNow;
                            state[i] = utcNow;
                            auditedEntity._CreatedOn = utcNow;
                            break;
                        }
                    }
                }
            }
            return false;
        }

    }

}
