using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore.Manifest
{
    [Serializable]
    public class ABManifest
    {
        public string Version { get => _version; internal set => _version = value; }

        [SerializeField]
        private string _version;

    }
}

