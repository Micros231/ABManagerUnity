using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ABManagerCore.Enums;
using ABManagerCore.Consts;

namespace ABManagerEditor.BuildModels
{
    public class ABLevelTemplate : ABTemplate
    {
        public ABResource SceneResource { get => _sceneResource; set => _sceneResource = value; }
        [SerializeField]
        private ABResource _sceneResource;
        protected override IEnumerable<AssetBundleBuild> GenerateBundleBuilds()
        {
            var assetsResourcesToBuild = Resources.Where(resource => resource.ResourceType == ResourceType.Asset);
            if (SceneResource.ResourceType != ResourceType.Scene)
            {
                throw new ArgumentException("SceneResource is not ResourceType.Scene");
            }
            var bundleBuilds = new List<AssetBundleBuild>();
            if (assetsResourcesToBuild != null && assetsResourcesToBuild.Count() > 0)
            {
                bundleBuilds.Add(CreateAssetBundleBuildToAssetResources(Names.LevelTemplate, assetsResourcesToBuild));
            }
            if (SceneResource != null)
            {
                bundleBuilds.Add(CreateAssetBundleBuildToSceneResources(Names.LevelTemplate, new[] { SceneResource }));
            }
            if (bundleBuilds.Count() > 0)
            {
                return bundleBuilds;
            }
            Debug.LogWarning($"Для Групы {Name} не будет производиться билд, потому что нет ассетов");
            return null;
        }
    }
}

