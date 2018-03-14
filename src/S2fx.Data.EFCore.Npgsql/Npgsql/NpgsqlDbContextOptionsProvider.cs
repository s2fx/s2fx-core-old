using System;
using System.Collections.Generic;
using System.Text;
using Npgsql.EntityFrameworkCore;

namespace S2fx.Data.EFCore.Npgsql {

    public class NpgsqlDbContextOptionsProvider : AbstractDbContextOptionsProvider {

        protected override S2DbContextOptions CreateOptions() {
            var connstr = "";
            var options = new S2DbContextOptions();
            //options.UseNpgsql();
            return options;
        }

    }
}
