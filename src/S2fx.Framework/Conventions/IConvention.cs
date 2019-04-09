using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Conventions {

    public interface IConvention<T> {

        void Apply(T model);
    }

}
