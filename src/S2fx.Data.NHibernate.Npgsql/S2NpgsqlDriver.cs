using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using NHibernate;
using NHibernate.Driver;
using NHibernate.SqlTypes;
using Npgsql;
using S2fx.Data.NHibernate.Npgsql.SqlTypes;

namespace S2fx.Data.NHibernate.Npgsql {

    public class S2NpgsqlDriver : NpgsqlDriver {

        protected override void InitializeParameter(DbParameter dbParam, string name, SqlType sqlType) {
            if (sqlType is AbstractNpgsqlSqlType npgSqltype && dbParam is NpgsqlParameter parameter) {
                this.InitializeNpgsqlParameter(parameter, name, npgSqltype);
            }
            else {
                base.InitializeParameter(dbParam, name, sqlType);
            }
        }

        protected virtual void InitializeNpgsqlParameter(
            NpgsqlParameter dbParam, string name, AbstractNpgsqlSqlType sqlType) {

            if (sqlType == null) {
                throw new QueryException($"No type assigned to parameter '{name}'");
            }
            dbParam.ParameterName = FormatNameForParameter(name);
            dbParam.DbType = sqlType.DbType;
            dbParam.NpgsqlDbType = sqlType.NpgsqlDbType;
        }
    }

}
