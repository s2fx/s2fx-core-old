using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate.SqlTypes;
using NpgsqlTypes;

namespace S2fx.Data.NHibernate.Npgsql.SqlTypes {

    public sealed class NpgsqlXmlSqlType : AbstractNpgsqlSqlType {

        public NpgsqlXmlSqlType() : base(DbType.Xml, NpgsqlDbType.Xml) {
        }
    }

}
