using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Data;
using S2fx.Model.Builtin;

namespace S2fx.Metadata.Services {

    public class TestService : ITestService {

        public TestService(ISafeRepository<FeatureEntity> repo) {
        }


        public string Hello() {
            return "Hello";
        }
    }
}
