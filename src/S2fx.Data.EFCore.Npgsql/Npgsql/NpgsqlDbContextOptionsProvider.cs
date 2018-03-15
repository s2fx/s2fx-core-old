using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;

namespace S2fx.Data.EFCore.Npgsql {

    public class NpgsqlDbContextOptionsProvider : AbstractDbContextOptionsProvider {

        protected override S2DbContextOptionsBuilder CreateOptionsBuilder() {
            var connstr = "Host=localhost;Database=s2fxdb;Username=s2fx;Password=s2fx";
            var builder = new S2DbContextOptionsBuilder();
            builder.UseNpgsql(connstr);
            //options.UseNpgsql();
            return builder;
        }

    }
}
