using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Interfaces;

namespace ABManagerCore.Manifest
{
    [Serializable]
    public class LevelTemplateInfo : ITemplateInfo
    {
        public string Name { get => _nameLevel; internal set => _nameLevel = value; }
        public SceneBundleInfo SceneBundleInfo { get => _sceneBundleInfo; internal set => _sceneBundleInfo = value; }
        public AssetsBundleInfo AssetsBundleInfo { get => _assetBundleInfo; internal set => _assetBundleInfo = value; }

        [SerializeField]
        private string _nameLevel;
        [SerializeField]
        private SceneBundleInfo _sceneBundleInfo = new SceneBundleInfo();
        [SerializeField]
        private AssetsBundleInfo _assetBundleInfo = new AssetsBundleInfo();
    }
}

