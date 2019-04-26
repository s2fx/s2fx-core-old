using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing {

    public interface IDataImporter {

        event EventHandler<EntityRecordImportedEventArgs> EntityRecordImported;

        Task ImportAsync(ImportingJobDescriptor descriptor);

        Task ImportAsync(IEnumerable<ImportingJobDescriptor> sortedDescriptors);

    }

}
