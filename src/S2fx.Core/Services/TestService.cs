using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Data;
using S2fx.Model.Entities;

namespace S2fx.Metadata.Services {

    public class TestService : ITestService {

        public TestService(IRepository<ModuleEntity> repo) {
        }


        public string Hello() {
            return "Hello";
        }
    }
}
