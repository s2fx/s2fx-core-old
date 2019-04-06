using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata.Conventions {

    public abstract class AbstractEntityFieldConvention : IMetadataConvention<MetaField> {

        public abstract void Apply(MetaField field);

    }

}
