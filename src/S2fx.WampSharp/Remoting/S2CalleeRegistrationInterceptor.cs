using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WampSharp.V2;
using WampSharp.V2.Core.Contracts;

namespace S2fx.Remoting {

    public class S2CalleeRegistrationInterceptor : ICalleeRegistrationInterceptor {

        public S2CalleeRegistrationInterceptor() {

        }

        public string GetProcedureUri(MethodInfo method) {
            throw new NotImplementedException();
        }

        public RegisterOptions GetRegisterOptions(MethodInfo method) {
            throw new NotImplementedException();
        }

        public bool IsCalleeProcedure(MethodInfo method) {
            throw new NotImplementedException();
        }

    }
}
