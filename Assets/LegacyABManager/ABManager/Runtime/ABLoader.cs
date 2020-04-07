using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using ABManagerCore;
using UnityEngine.Networking;
using System.Threading;
using System;

namespace ABManagerRuntime
{
    public class ABLoader
    {
        public static ABAsyncOperationHandle<ABManifest> LoadManifest(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                var ping = new Ping(url);
                if (ping.isDone)
                {
                    var request = UnityWebRequest.Get(url);
                    var operation = new ABAsyncOperationHandle<ABManifest>(request, new ABDownloadHandlerABManifest());
                    return operation;
                }
            }
            return new ABAsyncOperationHandle<ABManifest>();
        }
        public static ABAsyncOperationHandle<AssetBundle> LoadBundle(BundleInfo bundleInfo, ABManifest manifest)
        {
            if (manifest != null)
            {
                if (manifest.Bundles.FirstOrDefault(bundle => bundle == bundleInfo) != null)
                {
                    var request = UnityWebRequestAssetBundle.GetAssetBundle($"{manifest.RemoteLoadPath}/{bundleInfo.Name}");
                    var operation = new ABAsyncOperationHandle<AssetBundle>(request, new ABDownloadHandleAssetBundle());
                    return operation;
                }
            }
            return new ABAsyncOperationHandle<AssetBundle>();
        }
    }
}

