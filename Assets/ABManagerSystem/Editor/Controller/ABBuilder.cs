using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ABManagerCore.Manifest;

namespace ABManagerEditor.Controller
{
    public class ABBuilder
    {
        public void Build()
        {
            ABManifest manifest = new ABManifestBuilder().Version("0").Build();

            CreateFileManifest(manifest);
        }

        private void CreateFileManifest(ABManifest manifest)
        {
            if (manifest == null)
                throw new NullReferenceException("manifest object is null");
            string jsonString = EditorJsonUtility.ToJson(manifest);
            using (var streamWriter = File.CreateText(Path.Combine(Application.dataPath, "test-manifest.json")))
            {
                streamWriter.Write(jsonString);
            }
        }


    }
}

