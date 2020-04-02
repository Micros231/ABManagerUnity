using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

using ABManagerCore.Manifest;
using System.Text;

namespace ABManagerEditor.Controller
{
    public class ABBuilder : ABControllerAbstract
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
            string path = Path.Combine(Application.dataPath, "test-manifest.json");
            using (var createManifestStream = File.Create(path))
            using (var writer = new StreamWriter(createManifestStream))
            {
                writer.Write(jsonString);
            }
            WWWForm form = new WWWForm();
            using (var openManifestStream = File.OpenRead(path))
            using (var memoryStream = new MemoryStream())
            {
                openManifestStream.CopyTo(memoryStream);
                form.AddBinaryData("manifestFile", memoryStream.ToArray(), "test-manifest.json", "application/json");
            }

            var request = UnityWebRequest.Post("http://localhost:5000/content/uploadManifest", form);
            var operation = request.SendWebRequest();
            if (!request.isHttpError)
            {
                while (!operation.isDone)
                {
                    Debug.Log($"Upload Manifest progress: {operation.progress * 100}");
                }
            }
        }


    }
}

