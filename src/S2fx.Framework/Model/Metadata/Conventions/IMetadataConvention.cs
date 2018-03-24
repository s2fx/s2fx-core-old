using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata.Conventions {
    public interface IMetadataConvention {
    }

    public interface IMetadataConvention<T> : IMetadataConvention {
        void Apply(T metadata);
    }
}
