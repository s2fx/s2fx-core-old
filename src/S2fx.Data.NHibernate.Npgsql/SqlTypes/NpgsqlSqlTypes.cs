using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.SqlTypes;

namespace S2fx.Data.NHibernate.Npgsql.SqlTypes {
    public static class NpgsqlSqlTypes {

        public static SqlType XmlSqlType { get; } = new NpgsqlXmlSqlType();
        public static SqlType JsonSqlType { get; } = new NpgsqlJsonSqlType();
        public static SqlType JsonbSqlType { get; } = new NpgsqlJsonbSqlType();

    }
}
