using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Remoting {

    public class EntityResponse {
        public string EntityName { get; set; }
        public long Count { get; set; }
        public object Entities { get; set; }
        public IDictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
        public int ResultStatusCode { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

}
