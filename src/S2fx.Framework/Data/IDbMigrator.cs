using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data {

    public interface IDbMigrator {
        Task MigrateSchemaAsync();
    }
}
