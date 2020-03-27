using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ABManagerRuntime
{
    public class ABDownloadHandleAssetBundle : ABDownloadHandlerBase<AssetBundle>
    {
        public override AssetBundle GetContent(UnityWebRequest request)
        {
            Result = DownloadHandlerAssetBundle.GetContent(request);
            return Result;
        }
    }
}

