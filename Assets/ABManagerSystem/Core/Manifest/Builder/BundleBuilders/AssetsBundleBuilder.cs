using ABManagerCore.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore.Manifest.Builder
{
    public class AssetsBundleBuilder<TParent> : IChildBuilder<TParent>
    {
        private readonly AssetsBundleInfo _assetsBundleInfo;
        private readonly TParent _parentBuilder;
        public AssetsBundleBuilder(TParent parent, AssetsBundleInfo assetBundleInfo) 
        {
            _parentBuilder = parent;
            _assetsBundleInfo = assetBundleInfo;
        }
        public AssetsBundleBuilder<TParent> Name(string name)
        {
            _assetsBundleInfo.Name = name;
            return this;
        }
        public AssetsBundleBuilder<TParent> GUIDName(Guid guid)
        {
            _assetsBundleInfo.GUIDName = guid;
            return this;
        }
        public AssetsBundleBuilder<TParent> Hash(Hash128 hash)
        {
            _assetsBundleInfo.Hash = hash;
            return this;
        }
        public AssetsBundleBuilder<TParent> AddAsset(string name, string path)
        {
            AssetInfo assetInfo = new AssetInfo();
            assetInfo.Name = name;
            assetInfo.Path = path;
            _assetsBundleInfo.AssetsInfo.Add(assetInfo);
            return this;
        }
        public TParent Complete()
        {
            return _parentBuilder;
        }
    }

}
