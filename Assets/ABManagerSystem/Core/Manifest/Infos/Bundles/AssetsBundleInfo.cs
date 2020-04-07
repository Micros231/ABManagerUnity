using System;
using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Interfaces;

namespace ABManagerCore.Manifest
{
    [Serializable]
    public class AssetsBundleInfo : IBundleInfo
    {
        public string Name { get => _name; internal set => _name = value; }
        public Guid GUIDName { get => new Guid(_guidName); internal set => _guidName = value.ToString(); }
        public Hash128 Hash { get => Hash128.Parse(_hash); internal set => _hash = value.ToString(); }
        public List<AssetInfo> AssetsInfo { get => _assetsInfo; internal set => _assetsInfo = value; }


        [SerializeField]
        private string _name;
        [SerializeField]
        private string _guidName;
        [SerializeField]
        private string _hash;
        [SerializeField]
        private List<AssetInfo> _assetsInfo = new List<AssetInfo>();
    }
}

