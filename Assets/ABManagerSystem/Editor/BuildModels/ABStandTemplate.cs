using ABManagerCore.Consts;
using ABManagerCore.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.BuildModels
{
    public class ABStandTemplate : ABTemplate
    {
        protected override IEnumerable<AssetBundleBuild> GenerateBundleBuilds()
        {
            var assetsResourcesToBuild = Resources.Where(resource => resource.ResourceType == ResourceType.Asset);
            var bundleBuilds = new List<AssetBundleBuild>();
            if (assetsResourcesToBuild != null && assetsResourcesToBuild.Count() > 0)
            {
                bundleBuilds.Add(CreateAssetBundleBuildToAssetResources(Names.StandTemplate, assetsResourcesToBuild));
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

