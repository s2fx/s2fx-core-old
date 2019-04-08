using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Services {

    public interface IClock {
        DateTime UtcNow();
        DateTime Now();
    }

}
