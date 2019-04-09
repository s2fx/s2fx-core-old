using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IMetadataModelProvider {

        int Order { get; }

        void OnProvidersExecuting(MetadataModelProviderContext context);

        void OnProvidersExecuted(MetadataModelProviderContext context);

    }

}
