using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using S2fx.View.Schemas;

namespace S2fx.View {

    public interface IViewDefinitionsCollection : IList<ViewFile>, ICollection<ViewFile>, IEnumerable<ViewFile>, IEnumerable {
    }

}
