using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Interfaces;

namespace ABManagerCore.Manifest
{
    [Serializable]
    public class InstanceTemplateInfo : ITemplateInfo
    {
        public string Name { get => _instanceName; internal set => _instanceName = value; }
        public AssetsBundleInfo AssetsBundleInfo { get => _assetsBundleInfo; internal set => _assetsBundleInfo = value; }

        [SerializeField]
        private string _instanceName;
        [SerializeField]
        private AssetsBundleInfo _assetsBundleInfo = new AssetsBundleInfo();
    }
}

