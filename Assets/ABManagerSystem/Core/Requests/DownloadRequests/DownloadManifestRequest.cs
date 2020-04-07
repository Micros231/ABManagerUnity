using ABManagerCore.Manifest;
using System;
using UnityEngine.Networking;
using ABManagerCore.Helpers;
using ABManagerCore.Consts;
using UnityEngine;
using System.IO;

namespace ABManagerCore.Requests.Download
{
    public class DownloadManifestRequest : ABRequest<ABManifest>
    {
        public bool Cache { get; }
        protected DownloadManifestRequest(UnityWebRequest request, bool cache) : base(request) 
        {
            Cache = cache;
        }
        public static DownloadManifestRequest DownloadManifestByVersion(string version, bool cache = true)
        {
            var url = URLHelper.DownloadManifestByVersionURL(version);
            if (CheckIsValidUrl(url))
            {
                var request = UnityWebRequest.Get(url);
                var downloadManifestRequest = new DownloadManifestRequest(request, cache);
                return downloadManifestRequest;
            }
            return null;
        }
        protected override void OnRequestError(bool isNetworkError, bool isHttpError, string errorMessage, Action<string> errorHandler)
        {
            errorHandler?.Invoke(errorMessage);
        }
        protected override void OnRequestSuccess(UnityWebRequest response, Action<ABManifest> responseHandler)
        {
            string responseManifestText = response.downloadHandler.text;
            ABManifest manifest = JsonUtility.FromJson<ABManifest>(responseManifestText);
            if (Cache)
            {
                var pathDirectory = Path.Combine(Application.persistentDataPath, "_AppCache");
                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                var manifestFilePath = Path.Combine(pathDirectory, FileNames.ManifestFile);
                if (File.Exists(manifestFilePath))
                {
                    string cachedManifestString = string.Empty;
                    using (var cachedManifestStream = File.OpenText(manifestFilePath))
                    {
                        cachedManifestString = cachedManifestStream.ReadToEnd();
                    }
                    ABManifest cachedManifest = JsonUtility.FromJson<ABManifest>(cachedManifestString);
                    if (manifest.Version != cachedManifest.Version)
                    {
                        File.WriteAllText(manifestFilePath, responseManifestText);
                    }
                    else
                    {
                        manifest = cachedManifest;
                    }
                }
                else
                {
                    File.WriteAllText(manifestFilePath, responseManifestText);
                }
            }
            responseHandler?.Invoke(manifest);
        }
    }
}


