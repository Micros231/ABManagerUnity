using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore.Manifest.Builder
{
    public class InstanceTemplateBuilder : ChildBuilder<ABManifestBuilder, ABManifest>
    {
        private readonly InstanceTemplateInfo _instanceTemplateInfo;
        public InstanceTemplateBuilder(ABManifestBuilder parentBuilder, ABManifest manifest)
            : base(parentBuilder, manifest)
        {
            _instanceTemplateInfo = new InstanceTemplateInfo();
        }

        public InstanceTemplateBuilder Name(string Name)
        {
            _instanceTemplateInfo.Name = Name;
            return this;
        }

        public AssetsBundleBuilder<InstanceTemplateBuilder> AssetsBundle =>
            new AssetsBundleBuilder<InstanceTemplateBuilder>(this, _instanceTemplateInfo.AssetsBundleInfo);
        public override ABManifestBuilder Complete()
        {
            _parentElement.InstanceTemplatesInfo.Add(_instanceTemplateInfo);
            return _parentBuilder;
        }
    }
}

