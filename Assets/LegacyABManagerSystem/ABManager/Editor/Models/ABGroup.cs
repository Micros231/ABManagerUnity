using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ABManagerCore.Helpers;

namespace ABManagerEditor.Models
{
    public enum BuildCompressionType
    {
        LZMA = 1,
        LZ4 = 2,
        Uncompressed = 3
    }
    public class ABGroup : ABAbstract<ABAsset>
    {
        public string Name 
        { 
            get => _name; 
            set
            {
                _name = value;
                name = $"{value}_Group";
            }
        }
        public override string BuildPath 
        {
            get => _buildPath;
            set
            {
                if (_isCustomSettings)
                {
                    if (PathHelper.IsValidLocalPath(value, "Общий билд путь"))
                    {
                        _buildPath = value;
                    }
                }
                else
                    _buildPath = value;
            }
        }
        public override string LocalLoadPath 
        {
            get => _localLoadPath;
            set 
            {
                if (_isCustomSettings)
                {
                    if (PathHelper.IsValidLocalPath(value, "Общий локальный путь загрузки"))
                    {
                        _localLoadPath = value;
                    }
                }
                else
                    _localLoadPath = value;
            }
        }
        public override string RemoteLoadPath 
        {
            get => _remoteLoadPath;
            set 
            {
                if (_isCustomSettings)
                {
                    if (PathHelper.IsValidRemotePath(value, "Общий удаленный путь загрузки"))
                    {
                        _remoteLoadPath = value;
                    }
                }
                else
                   _remoteLoadPath = value;
            }
        }
        public bool IsCustomSettings { get => _isCustomSettings; set => _isCustomSettings = value; }
        public BuildCompressionType BuildCompressionType 
        { 
            get => _buildCompressionType; 
            set => _buildCompressionType = value; 
        }
        public BuildCompression Compression 
        { 
            get 
            {
                BuildCompression compression;
                switch (BuildCompressionType)
                {
                    case BuildCompressionType.LZMA:
                        compression = BuildCompression.LZMA;
                        break;
                    case BuildCompressionType.LZ4:
                        compression = BuildCompression.LZ4;
                        break;
                    case BuildCompressionType.Uncompressed:
                        compression = BuildCompression.Uncompressed;
                        break;
                    default:
                        compression = BuildCompression.LZMA;
                        break;
                }
                return compression;
            } 
        }
        public IEnumerable<AssetBundleBuild> BundleBuilds 
        { 
            get
            {
                
                var assetsBuilds = Items.Where(asset => asset.TypeAsset == TypeAsset.Default);
                var scenesBuilds = Items.Where(asset => asset.TypeAsset == TypeAsset.Scene);
                var bundleBuilds = new List<AssetBundleBuild>();
                if (assetsBuilds != null && assetsBuilds.Count() > 0)
                {
                    bundleBuilds.Add(CreateAssetBundleBuild($"all_assets(group-{Name}).bundle", assetsBuilds));
                }
                if (scenesBuilds != null && scenesBuilds.Count() > 0)
                {
                   bundleBuilds.Add(CreateAssetBundleBuild($"all_scenes(group-{Name}).bundle", scenesBuilds));
                }
                if (bundleBuilds.Count() > 0)
                {
                    return bundleBuilds;
                }
                Debug.LogWarning($"Для Групы {Name} не будет производиться билд, потому что нет ассетов");
                return null;
            } 
        }      

        #region Private SerializeFields
        [SerializeField]
        private string _name;
        [SerializeField]
        private bool _isCustomSettings;
        [SerializeField]
        private BuildCompressionType _buildCompressionType = BuildCompressionType.LZMA;
        #endregion
        private AssetBundleBuild CreateAssetBundleBuild(string name, IEnumerable<ABAsset> assets)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "AsetBundleBuildName is empty or null");
            }
            var bundleBuild = new AssetBundleBuild();
            bundleBuild.assetBundleName = name;
            string[] assetsPaths = assets.Select(asset => asset.PathAsset).ToArray();
            string[] assetsNames = assets.Select(asset => asset.Name).ToArray();
            bundleBuild.assetNames = assetsPaths;
            bundleBuild.addressableNames = assetsNames;
            return bundleBuild;
        }
    }
}


