using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using S2fx.View.Schemas;

namespace S2fx.View.Model {

    public class ActionInfo {

        [DataMember]
        public long Id { get; }

        [DataMember]
        public string ActionType { get; }

        [DataMember]
        public bool CanBeHome { get; }

        [DataMember]
        public int Priority { get; }

        [DataMember]
        public AbstractActionDefinition Definition { get; }

        public ActionInfo(long id, string actionType, bool canBeHome, int priority, AbstractActionDefinition definition) {
            this.Id = id;
            this.ActionType = actionType;
            this.CanBeHome = canBeHome;
            this.Priority = priority;
            this.Definition = definition;
        }
    }

}
