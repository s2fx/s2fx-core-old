using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate.SqlTypes;
using NpgsqlTypes;

namespace S2fx.Data.NHibernate.Npgsql.SqlTypes {

    public abstract class AbstractNpgsqlSqlType : SqlType {

        public NpgsqlDbType NpgsqlDbType { get; }

        public AbstractNpgsqlSqlType(DbType dbType, NpgsqlDbType npgDbType)
            : base(dbType) {
            NpgsqlDbType = npgDbType;
        }

        public AbstractNpgsqlSqlType(DbType dbType, NpgsqlDbType npgDbType, int length)
            : base(dbType, length) {
            NpgsqlDbType = npgDbType;
        }

        public AbstractNpgsqlSqlType(DbType dbType, NpgsqlDbType npgDbType, byte precision, byte scale)
            : base(dbType, precision, scale) {
            NpgsqlDbType = npgDbType;
        }

    }

}
