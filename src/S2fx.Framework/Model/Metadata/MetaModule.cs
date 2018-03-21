using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Modules;
using S2fx.Environment.Extensions;

namespace S2fx.Model.Metadata {

    public class MetaModule : AnyMetadata {

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string State { get; set; }

        public Version Version { get; set; }

        public Module OrchardModuleInfo { get; set; }

        public S2ModuleAttribute S2ModuleInfo => this.OrchardModuleInfo.ModuleInfo as S2ModuleAttribute;

        public override void AcceptVisitor(IModelMetadataVisitor visitor) =>
            visitor.VisitModule(this);

    }

}
