using ABManagerCore.Manifest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UniRx.Async;

namespace ABManagerRuntime.Loader
{
    public static class SimpleLoader
    {
        public static void LoadManifest(string version, Action<ABManifest> responseHandler, Action<float> responseProgress = null)
        {
            if (!CheckVersionIsNullOrEmpty(version))
            {
                var request = UnityWebRequest.Get($"http://localhost:5000/content/{version}/manifest");
                var progress = Progress.Create(responseProgress);
                var uniTask = request.SendWebRequest().ConfigureAwait(progress: progress);
                ExecuteLoadManifest(uniTask, responseHandler).Forget();
            }
            
        }
        public static ResponseHandler<ABManifest> LoadManifest(string version, Action<float> responseProgress = null)
        {
            if (!CheckVersionIsNullOrEmpty(version))
            {
                var request = UnityWebRequest.Get($"http://localhost:5000/content/{version}/manifest");
                var progress = Progress.Create(responseProgress);
                var uniTask = request.SendWebRequest().ConfigureAwait(progress: progress);
                ResponseHandler<ABManifest> handler = new ResponseHandler<ABManifest>(responseProgress);
                ExecuteLoadManifest(uniTask, handler.ResponceHandler).Forget();
                return handler;
            }
            return null;
        }
        private static async UniTaskVoid ExecuteLoadManifest(UniTask<UnityWebRequest> uniTask, Action<ABManifest> responseHandler)
        {
            await uniTask;
            if (uniTask.IsCompleted)
            {
                var response = uniTask.Result;
                if (response.isHttpError || response.isNetworkError)
                {
                    Debug.LogError(response.error);
                }
                if (response.isDone)
                {
                    var manifestText = response.downloadHandler.text;
                    ABManifest manifest = JsonUtility.FromJson<ABManifest>(manifestText);
                    responseHandler?.Invoke(manifest);
                }
            }
        }
        private static bool CheckVersionIsNullOrEmpty(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                Debug.LogError("Version empty or null");
                return true;
            }
            return false;
        }
    }
}

