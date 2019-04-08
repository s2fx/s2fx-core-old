//Adapted from: https://github.com/Viostream/nhibernate-json/blob/master/src/NHibernate.Json/JsonColumnType.cs
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace S2fx.Data.NHibernate.Types {

    public class TextBasedXmlObjectType<T> : IUserType
        where T : class {

        readonly XmlSerializer _serializer = new XmlSerializer(typeof(T));

        static readonly SqlType[] s_sqlTypes = new SqlType[] { NHibernateUtil.StringClob.SqlType };

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
            var xml = returnValue == null ? string.Empty : returnValue.ToString();
            return Deserialise(xml);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session) {
            var column = value as T;
            if (value == null) {
                NHibernateUtil.String.NullSafeSet(cmd, string.Empty, index, session);
                return;
            }
            value = Serialise(column);
            NHibernateUtil.String.NullSafeSet(cmd, value, index, session);
        }

        public object Replace(object original, object target, object owner) => original;


        public T Deserialise(string xmlString) {
            if (string.IsNullOrWhiteSpace(xmlString))
                return CreateObject(typeof(T));
            return (T)_serializer.Deserialize(new StringReader(xmlString));
        }

        public string Serialise(T obj) {
            if (obj == null) {
                return string.Empty;
            }
            using (var sw = new StringWriter()) {
                _serializer.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        private static T CreateObject(Type jsonType) {
            var result = Activator.CreateInstance(jsonType, true);
            return (T)result;
        }

    }

}
