using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing {

    public interface IRecordImporter {
        Task InsertOrUpdateEntityAsync(ImportContext context, object record, bool canUpdate);
    }

}
