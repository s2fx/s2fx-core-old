using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.View.Schemas;

namespace S2fx.View.RemoteServices {

    public class ViewInfo {

        public string Name { get; }
        public AbstractEntityViewDefinition View { get; }
        public IEnumerable<MetaField> MetaFields { get; }

        public ViewInfo(
            string name,
            AbstractEntityViewDefinition view,
            IEnumerable<MetaField> metaFields = null) {
            this.Name = name;
            this.View = view;
            this.MetaFields = metaFields;
        }
    }
}
