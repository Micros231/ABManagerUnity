using ABManagerCore.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore.Manifest.Builder
{
    public class SceneBundleBuilder<TParent> : IChildBuilder<TParent>
    {
        private readonly SceneBundleInfo _sceneBundleInfo;
        private readonly TParent _parentBuilder;
        public SceneBundleBuilder(TParent parent, SceneBundleInfo sceneBundleInfo)
        {
            _parentBuilder = parent;
            _sceneBundleInfo = sceneBundleInfo;
        }
        public SceneBundleBuilder<TParent> Name(string name)
        {
            _sceneBundleInfo.Name = name;
            return this;
        }
        public SceneBundleBuilder<TParent> GUIDName(Guid guid)
        {
            _sceneBundleInfo.GUIDName = guid;
            return this;
        }
        public SceneBundleBuilder<TParent> Hash(Hash128 hash)
        {
            _sceneBundleInfo.Hash = hash;
            return this;
        }
        public SceneBundleBuilder<TParent> Scene(string name, string path)
        {
            SceneInfo sceneInfo = new SceneInfo();
            sceneInfo.Name = name;
            sceneInfo.Path = path;
            _sceneBundleInfo.SceneInfo = sceneInfo;
            return this;
        }

        public TParent Complete()
        {
            return _parentBuilder;
        }
    }
}

