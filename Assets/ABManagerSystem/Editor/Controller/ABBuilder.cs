using System;
using UnityEngine;
using UnityEditor;
using ABManagerCore.Manifest;
using ABManagerCore.Manifest.Builder;
using ABManagerCore.Requests.Download;
using ABManagerCore.Consts;
using System.IO;
using ABManagerCore.Requests.Upload;
using UnityEditor.Build.Pipeline.Utilities;

namespace ABManagerEditor.Controller
{
    internal class ABBuilder : ABControllerAbstract
    {
        internal void Build()
        {
            ABManifestBuilder manifestBuilder = new ABManifestBuilder();
            manifestBuilder.Version(ManagerSettings.Version);
            manifestBuilder.PlatformTarget(ManagerSettings.BuildTarget);
            var name = "BundleName1";
            var hash = HashingMethods.Calculate(name).ToHash128();
            var guid = HashingMethods.Calculate(name).ToGUID();
            manifestBuilder.NewInstanceTemplate()
                .Name("InstanceTest1")
                .AssetsBundle.Name(name).GUIDName(new Guid(guid.ToString())).Hash(hash)
                .AddAsset("asset1", "path/asset1").Complete().Complete();
            
            ABManifest manifest = manifestBuilder.Build();
            CreateFileManifest(manifest);
        }
        private void CreateFileManifest(ABManifest manifest)
        {
            if (manifest == null)
                throw new NullReferenceException("manifest object is null");
            string jsonString = EditorJsonUtility.ToJson(manifest);
            string directoryPath = Path.Combine(ManagerSettings.BuildPath, ManagerSettings.Version, ManagerSettings.BuildTarget.ToString());
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string manifestPath = Path.Combine(directoryPath, FileNames.ManifestFile);
            using (var manifestStream = File.Create(manifestPath))
            using (var writer = new StreamWriter(manifestStream))
            {
                writer.Write(jsonString);
            }
            var request = UploadManifestRequest.UploadManifest(manifestPath).SendRequest();

            request.OnCompleted += ManifestUploadOnComplete;
        }
        private void ManifestUploadOnComplete(string obj)
        {
            Debug.Log(obj);
            int version = int.Parse(ManagerSettings.Version);
            version++;
            ManagerSettings.Version = version.ToString();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

}

