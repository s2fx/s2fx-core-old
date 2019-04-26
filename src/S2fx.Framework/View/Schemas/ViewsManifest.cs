using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(ViewFiles))]
    public class ViewsManifest {

        public List<ViewFile> ViewFiles { get; } = new List<ViewFile>();

    }

}
