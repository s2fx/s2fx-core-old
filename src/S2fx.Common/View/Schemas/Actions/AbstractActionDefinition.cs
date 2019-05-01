using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace S2fx.View.Schemas {

    public abstract class AbstractActionDefinition : Element, IViewDefinition {

        [DataMember]
        public abstract string ActionType { get; }

        [Required, DataMember]
        public string Entity { get; set; }

        [Required, DataMember]
        public int Priority { get; set; } = 0;

        [Required, DataMember]
        public string Name { get; set; }

        [IgnoreDataMember]
        public string Feature { get; set; } = null;

        [Required, DataMember]
        public string Text { get; set; }
    }

}
