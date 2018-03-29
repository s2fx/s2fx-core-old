using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing {

    public class EntityRecordImportedEventArgs : AbstractEntityEventArgs {

        public object Record { get; }

        public EntityRecordImportedEventArgs(MetaEntity entity, object record) : base(entity) {
            this.Record = record;
        }

    }

}
