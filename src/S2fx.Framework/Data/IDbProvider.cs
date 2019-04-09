using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data {

    public interface IDbProvider {
        int Order { get; }
        string Name { get; }
        string DisplayName { get; }
        bool IsLoginNeeded { get; }
    }

}
