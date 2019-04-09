using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IFieldValueConverter {

        bool TryParse(MetaField field, string value, out object result, string format = null);

    }
}
