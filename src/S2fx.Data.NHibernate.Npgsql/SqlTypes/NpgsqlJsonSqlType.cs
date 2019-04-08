using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate.SqlTypes;
using NpgsqlTypes;

namespace S2fx.Data.NHibernate.Npgsql.SqlTypes {

    public sealed class NpgsqlJsonSqlType : AbstractNpgsqlSqlType {

        public NpgsqlJsonSqlType() : base(DbType.Object, NpgsqlDbType.Json) {
        }
    }

}
