using System;
using UnityEngine;
using ABManagerCore.Interfaces;

namespace ABManagerCore.Manifest
{
    [Serializable]
    public class AssetInfo : IResourceInfo
    {
        public string Name { get => _name; internal set => _name = value; }
        public string Path { get => _path; internal set => _path = value; }

        [SerializeField]
        private string _name;
        [SerializeField]
        private string _path;
    }

}
