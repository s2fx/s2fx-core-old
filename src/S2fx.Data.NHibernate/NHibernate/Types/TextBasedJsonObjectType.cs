//Adapted from: https://github.com/Viostream/nhibernate-json/blob/master/src/NHibernate.Json/JsonColumnType.cs
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using NHibernate;
using Newtonsoft.Json;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace S2fx.Data.NHibernate.Types {

    public class TextBasedJsonObjectType<T> : IUserType
        where T : class {

        private static readonly SqlType[] s_sqlTypes = new SqlType[] { NHibernateUtil.String.SqlType };

        public SqlType[] SqlTypes => s_sqlTypes;

        public Type ReturnedType => typeof(T);

        public bool IsMutable => false;

        public object Assemble(object cached, object owner) => cached;

        public object Disassemble(object value) => value;

        public object DeepCopy(object value) {
            var source = value as T;
            if (source == null) {
                return null;
            }
            return Deserialise(Serialise(source));
        }

        public new bool Equals(object x, object y) {
            var left = x as T;
            var right = y as T;

            if (left == null && right == null)
                return true;

            if (left == null || right == null)
                return false;

            return Serialise(left).Equals(Serialise(right));
        }

        public int GetHashCode(object x) => x.GetHashCode();

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner) {
            var returnValue = NHibernateUtil.String.NullSafeGet(rs, names[0], session);
            var json = returnValue == null ? "{}" : returnValue.ToString();
            return Deserialise(json);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session) {
            var column = value as T;
            if (value == null) {
                NHibernateUtil.String.NullSafeSet(cmd, "{}", index, session);
                return;
            }
            value = Serialise(column);
            NHibernateUtil.String.NullSafeSet(cmd, value, index, session);
        }

        public object Replace(object original, object target, object owner) => original;


        public T Deserialise(string jsonString) {
            if (string.IsNullOrWhiteSpace(jsonString))
                return CreateObject(typeof(T));
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public string Serialise(T obj) {
            if (obj == null) {
                return "{}";
            }
            return JsonConvert.SerializeObject(obj);
        }

        private static T CreateObject(Type jsonType) {
            var result = Activator.CreateInstance(jsonType, true);
            return (T)result;
        }

    }

}
