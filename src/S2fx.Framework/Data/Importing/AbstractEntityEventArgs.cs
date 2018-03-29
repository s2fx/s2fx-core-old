using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing {

    public abstract class AbstractEntityEventArgs : EventArgs {
        public MetaEntity Entity { get; }

        public AbstractEntityEventArgs(MetaEntity entity) {
            this.Entity = entity;
        }

    }

}
