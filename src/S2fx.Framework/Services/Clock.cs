using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Services {

    public class Clock : IClock {
        public DateTime Now() => DateTime.Now;
        public DateTime UtcNow() => DateTime.UtcNow;
    }
}
