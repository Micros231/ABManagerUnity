using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using ABManagerCore.Interfaces;
using ABManagerCore.Enums;
using ABManagerCore.Consts;
using ABManagerCore.Helpers;

namespace ABManagerEditor.BuildModels
{
    public abstract class ABTemplate : ScriptableObject, ITemplateInfo
    {
        public string Name { get => _name; internal set => _name = value; }
        public IEnumerable<AssetBundleBuild> BundleBuilds => GenerateBundleBuilds();
        public IList<ABResource> Resources => _resources;
        [SerializeField]
        private string _name;
        [SerializeField]
        private List<ABResource> _resources = new List<ABResource>();
        protected abstract IEnumerable<AssetBundleBuild> GenerateBundleBuilds();

        protected AssetBundleBuild CreateAssetBundleBuildToAssetResources(string templateName, IEnumerable<ABResource> assetResources)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                throw new ArgumentNullException("name", "AsetBundleBuildName is empty or null");
            }
            IList<ABResourceBase> resourcesToABBuild = new List<ABResourceBase>();
            IList<ABResource> sortedABResources = new List<ABResource>(assetResources);
            SortABResources(ref sortedABResources);
            SortResourcesToABBuild_First(ref resourcesToABBuild, sortedABResources);
            foreach (var resource in sortedABResources)
            {
                SortResourcesToABBuild_Second(ref resourcesToABBuild, resource.DependenciesResources);
            }
            var bundleBuild = 
                GenerateAssetBundleBuild(
                    FileNameHelper.GetAssetBundleFileName(templateName, Names.all_assets, Name), 
                    resourcesToABBuild);
            return bundleBuild;
        }
        protected AssetBundleBuild CreateAssetBundleBuildToSceneResources(string templateName, IEnumerable<ABResource> sceneResources)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                throw new ArgumentNullException("name", "AsetBundleBuildName is empty or null");
            }
            foreach (var sceneResource in sceneResources)
            {
                if (sceneResource.ResourceType != ResourceType.Scene)
                {
                    throw new ArgumentException("SceneResource is not ResourceType.Scene");
                }
            }
            IList<ABResource> sortedABResources = new List<ABResource>(sceneResources);
            SortABResources(ref sortedABResources);
            var bundleBuild =
                GenerateAssetBundleBuild(
                    FileNameHelper.GetAssetBundleFileName(templateName, Names.all_scenes, Name),
                    sortedABResources);
            return bundleBuild;
        }
        private AssetBundleBuild GenerateAssetBundleBuild(string name, IEnumerable<ABResourceBase> resourcesToABBuild)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "AsetBundleBuildName is empty or null");
            }
            var bundleBuild = new AssetBundleBuild();
            bundleBuild.assetBundleName = name;
            string[] assetsPaths = resourcesToABBuild.Select(asset => asset.Path).ToArray();
            string[] assetsNames = resourcesToABBuild.Select(asset => asset.Name).ToArray();
            bundleBuild.assetNames = assetsPaths;
            bundleBuild.addressableNames = assetsNames;
            return bundleBuild;
        }
        private void SortABResources(ref IList<ABResource> resourcesToABBuild)
        {
            for (int i = 0; i < resourcesToABBuild.Count; i++)
            {
                var resourceToABBuild = resourcesToABBuild[i];
                for (int y = 0; y < resourcesToABBuild.Count; y++)
                {
                    var resourceToCompare = resourcesToABBuild[y];
                    if (resourceToABBuild != resourceToCompare && resourceToABBuild.Equals(resourceToCompare))
                    {
                        resourcesToABBuild.Remove(resourceToCompare);
                    }
                }
            }
        }
        private void SortResourcesToABBuild_First(ref IList<ABResourceBase> resourcesToABBuild, IEnumerable<ABResource> resources)
        {
            foreach (var resource in resources)
            {
                resourcesToABBuild.Add(resource);
            }
        }
        private void SortResourcesToABBuild_Second(ref IList<ABResourceBase> resourcesToABBuild, IEnumerable<ABResourceDep> resourcesDep)
        {
            foreach (var resourceDep in resourcesDep)
            {
                foreach (var resourceToABBuild in resourcesToABBuild)
                {
                    if (!resourceDep.Equals(resourceToABBuild))
                    {
                        resourcesToABBuild.Add(resourceDep);
                    }
                }
            }
        }
    }
}

