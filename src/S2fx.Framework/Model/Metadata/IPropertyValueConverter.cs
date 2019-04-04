using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IPropertyValueConverter {

        bool TryParseFieldValue(MetaField property, string value, out object result, string format = null);

    }
}
