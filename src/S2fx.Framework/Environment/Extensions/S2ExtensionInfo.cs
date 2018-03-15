using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Extensions.Features;

namespace S2fx.Environment.Extensions {

    public interface IS2ExtensionInfo : IExtensionInfo { }

    public class S2ExtensionInfo : IS2ExtensionInfo {

        private readonly IExtensionInfo _orchardExtensionInfo;

        public S2ExtensionInfo(IExtensionInfo orchardExtensionInfo) {
            _orchardExtensionInfo = orchardExtensionInfo;
        }

        public string Id => _orchardExtensionInfo.Id;

        public string SubPath => _orchardExtensionInfo.SubPath;

        public bool Exists => _orchardExtensionInfo.Exists;

        public IManifestInfo Manifest => _orchardExtensionInfo.Manifest;

        public IEnumerable<IFeatureInfo> Features => _orchardExtensionInfo.Features;
    }

}
