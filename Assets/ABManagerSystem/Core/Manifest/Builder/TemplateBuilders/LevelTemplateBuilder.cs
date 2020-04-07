using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Interfaces;

namespace ABManagerCore.Manifest.Builder
{
    public class LevelTemplateBuilder : ChildBuilder<ABManifestBuilder, ABManifest>
    {
        private readonly LevelTemplateInfo _levelTemplateInfo;
        public LevelTemplateBuilder(ABManifestBuilder parentBuilder, ABManifest manifest)
            : base(parentBuilder, manifest)
        {
            _levelTemplateInfo = new LevelTemplateInfo();
        }

        public LevelTemplateBuilder Name(string Name)
        {
            _levelTemplateInfo.Name = Name;
            return this;
        }

        public AssetsBundleBuilder<LevelTemplateBuilder> AssetsBundle => 
            new AssetsBundleBuilder<LevelTemplateBuilder>(this, _levelTemplateInfo.AssetsBundleInfo);
        public SceneBundleBuilder<LevelTemplateBuilder> SceneBundle =>
            new SceneBundleBuilder<LevelTemplateBuilder>(this, _levelTemplateInfo.SceneBundleInfo);
        public override ABManifestBuilder Complete()
        {
            _parentElement.LevelTemplatesInfo.Add(_levelTemplateInfo);
            return _parentBuilder;
        }
    }
}

