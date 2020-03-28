using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore.Manifest
{
    public class ABManifestBuilder
    {
        private readonly ABManifest _manifest;

        public ABManifestBuilder()
        {
            _manifest = new ABManifest();
        }

        public ABManifestBuilder Version(string version)
        {
            _manifest.Version = version;
            return this;
        }

        public ABManifest Build()
        {
            return _manifest;
        }
    }
}

