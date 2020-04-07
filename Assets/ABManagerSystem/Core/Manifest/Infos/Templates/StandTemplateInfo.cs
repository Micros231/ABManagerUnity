using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Interfaces;

namespace ABManagerCore.Manifest
{
    [Serializable]
    public class StandTemplateInfo : ITemplateInfo
    {
        public string Name { get => _standName; internal set => _standName = value; }
        public AssetsBundleInfo AssetsBundleInfo { get => _assetsBundleInfo; internal set => _assetsBundleInfo = value; }

        [SerializeField]
        private string _standName;
        [SerializeField]
        private AssetsBundleInfo _assetsBundleInfo = new AssetsBundleInfo();
    }
}

