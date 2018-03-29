using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Remoting.Conventions {

    public interface IWampHostConvention {
        Uri GetHostingUri();
    }

}
