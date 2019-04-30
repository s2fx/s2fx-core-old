using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing {

    public interface IRecordFinder {
        Task<object> FindExistedRecordOrDefaultAsync(ImportingJob importingTask, IReadOnlyDictionary<string, object> symbols);
    }

}
