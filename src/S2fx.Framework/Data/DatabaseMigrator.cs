using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data {

    public interface IDatabaseMigrator {

        Task MigrateSchemeAsync();

    }

    public class DatabaseMigrator : IDatabaseMigrator {

        public Task MigrateSchemeAsync() {
            throw new NotImplementedException();
        }

    }
}
