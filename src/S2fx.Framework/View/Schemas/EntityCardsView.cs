using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.View.Schemas {

    [ViewElement("Cards")]
    public class EntityCardsView : EntityViewDefinition {
        public override string ViewType => "Card";
    }

}
