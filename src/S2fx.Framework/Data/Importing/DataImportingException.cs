using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing {

    public class DataImportingException : Exception {

        public DataImportingException(string message, string file = null, string selector = null) : base(message) {
            this.File = file;
            this.Selector = selector;
        }

        public DataImportingException() {

        }

        public string File { get; set; }

        public string Selector { get; set; }

    }

}
