using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data {

    public interface IDbProvider {
        bool IsSupportXmlColumn { get; }
        bool IsSupportJsonbColumn { get; }
    }

}
