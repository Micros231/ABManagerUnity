using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore.Manifest.Builder
{
    public class StandTemplateBuilder : ChildBuilder<ABManifestBuilder, ABManifest>
    {
        private readonly StandTemplateInfo _standTemplateInfo;
        public StandTemplateBuilder(ABManifestBuilder parentBuilder, ABManifest manifest)
            : base(parentBuilder, manifest)
        {
            _standTemplateInfo = new StandTemplateInfo();
        }

        public StandTemplateBuilder Name(string Name)
        {
            _standTemplateInfo.Name = Name;
            return this;
        }

        public AssetsBundleBuilder<StandTemplateBuilder> AssetsBundle =>
            new AssetsBundleBuilder<StandTemplateBuilder>(this, _standTemplateInfo.AssetsBundleInfo);

        public override ABManifestBuilder Complete()
        {
            _parentElement.StandTemplatesInfo.Add(_standTemplateInfo);
            return _parentBuilder;
        }
    }
}

