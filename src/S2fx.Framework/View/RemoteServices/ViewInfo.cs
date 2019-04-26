using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.View.Schemas;

namespace S2fx.View.RemoteServices {

    public class ViewInfo {

        public string Name { get; }
        public string ViewType { get; }
        public AbstractEntityViewDefinition View { get; }
        public IReadOnlyDictionary<string, MetaField> MetaFields { get; }

        public ViewInfo(
            string name,
            string viewType,
            AbstractEntityViewDefinition view,
            IReadOnlyDictionary<string, MetaField> metaFields = null) {
            this.Name = name;
            this.ViewType = viewType;
            this.View = view;
            this.MetaFields = metaFields;
        }
    }
}
