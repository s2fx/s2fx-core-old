using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing {

    public interface IDataImporter {

        Task ImportAsync(ImportJob job);
        Task ImportAsync(IEnumerable<ImportJob> jobs);

    }

}
