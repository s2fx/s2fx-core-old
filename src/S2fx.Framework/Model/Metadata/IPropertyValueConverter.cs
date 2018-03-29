using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IPropertyValueConverter {

        bool TryParsePropertyValue(string value, out object result);

    }
}
