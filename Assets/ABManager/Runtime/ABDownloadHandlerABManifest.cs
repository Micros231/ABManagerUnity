using ABManagerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ABManagerRuntime
{
    public class ABDownloadHandlerABManifest : ABDownloadHandlerBase<ABManifest>
    {
        public override ABManifest GetContent(UnityWebRequest request)
        {
            if (request.isDone && !request.isHttpError)
            {
                var manifest = GetManifest(request.downloadHandler.text);
                if (manifest != null)
                {
                    return manifest;
                }
            }
            return null;
        }

        private ABManifest GetManifest(string jsonString)
        {
            if (!string.IsNullOrEmpty(jsonString))
            {
                return JsonUtility.FromJson<ABManifest>(jsonString);
            }
            return null;
        }
        
    }
}

