using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using ABManagerRuntime;

public class LoadBundle : MonoBehaviour
{

    [SerializeField]
    private string url;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ABLoader loader = new ABLoader();
            var operation = ABLoader.LoadManifest(url);
            operation.Completed += LoadManifest_Completed;
            StartCoroutine(operation);
        }
    }

    private void LoadManifest_Completed(ABAsyncOperationHandle<ABManagerCore.ABManifest> obj)
    {
        ABLoader loader = new ABLoader();
        var operation = ABLoader.LoadBundle(obj.Result.Bundles[0], obj.Result);
        operation.Completed += LoadBundle_Completed;
        StartCoroutine(operation);

    }

    private void LoadBundle_Completed(ABAsyncOperationHandle<AssetBundle> obj)
    {
        foreach (var item in obj.Result.GetAllAssetNames())
        {
            var newObject = obj.Result.LoadAsset<GameObject>(item);
            Instantiate(newObject);
        }
    }
}
