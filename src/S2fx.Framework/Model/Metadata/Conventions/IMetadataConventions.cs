using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Conventions;

namespace S2fx.Model.Metadata.Conventions {

    public interface IMetadataConvention {
    }

    public interface IMetadataConvention<T> : IMetadataConvention, IConvention<T>
        where T : IMetadataNode {
    }

    public interface IMetadataModelConvention : IMetadataConvention<MetadataModel> {
    }

    public interface IMetadataEntityConvention : IMetadataConvention<MetaEntity> {
    }

    public interface IMetadataFieldConvention : IMetadataConvention<MetaField> {
    }
}
