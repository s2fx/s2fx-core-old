using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata.Conventions {

    public abstract class AbstractEntityConvention : IMetadataConvention<MetaEntity> {

        public abstract void Apply(MetaEntity entity);

    }

}
