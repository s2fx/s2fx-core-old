using System;
using System.Collections.Generic;
using System.Text;
using S2fx.View.Schemas;

namespace S2fx.View {

    public static class ViewDefinitionsCollectionExtensions {

        public static IViewDefinitionsCollection AddViewFile(this IViewDefinitionsCollection self, string path) {
            self.Add(new ViewFile { Path = path });
            return self;
        }

    }

}
