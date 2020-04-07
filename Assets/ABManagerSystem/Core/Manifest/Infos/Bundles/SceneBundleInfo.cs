using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Interfaces;

namespace ABManagerCore.Manifest
{
    [Serializable]
    public class SceneBundleInfo : IBundleInfo
    {
        public string Name { get => _name; internal set => _name = value; }
        public Guid GUIDName { get => _guidName; internal set => _guidName = value; }
        public Hash128 Hash { get => _hash; internal set => _hash = value; }
        public SceneInfo SceneInfo { get => _sceneInfo; internal set => _sceneInfo = value; }


        [SerializeField]
        private string _name;
        [SerializeField]
        private Guid _guidName;
        [SerializeField]
        private Hash128 _hash;
        [SerializeField]
        private SceneInfo _sceneInfo;
    }
}

