using ABManagerCore;
using ABManagerRuntime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ABLoadSample : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvas;
    [SerializeField]
    private ABManifest _manifest;
    [SerializeField]
    private string _remoteManifestPath;
    [SerializeField]
    private Text _textManifestVersion;
    [SerializeField]
    private Text _textManifestName;
    [SerializeField]
    private Text _textManifestRemote;
    [SerializeField]
    private Text _textManifestLocal;
    [SerializeField]
    private InputField _inputFieldManifestPath;
    [SerializeField]
    private Transform _containerAssets;
    [SerializeField]
    private GameObject _panelAssetsPrefab;
    [SerializeField]
    private Transform _containerScenes;
    [SerializeField]
    private GameObject _panelScenesPrefab;

    private AssetBundle _assetsBundle;
    private AssetBundle _scenesBundle;
    void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(_canvas);
        _inputFieldManifestPath.onValueChanged.AddListener((text) =>
        {
            _remoteManifestPath = text;
        });
    }

    public void LoadManifest()
    {
        if (!string.IsNullOrEmpty(_remoteManifestPath))
        {
            var operation = ABLoader.LoadManifest(_remoteManifestPath);
            operation.Completed += LoadManifest_Completed;
            StartCoroutine(operation);
        }
    }
    public void LoadAssets()
    {
        RemoveAssets();
        if (_manifest != null && _manifest.Assets != null)
        {
            var bundleInfo = _manifest.Bundles.FirstOrDefault(bundle => bundle.Name.Contains("all_assets"));
            if (bundleInfo != null)
            {
                var operation = ABLoader.LoadBundle(bundleInfo, _manifest);
                operation.Completed += LoadBundleAssets_Comleted;
                StartCoroutine(operation);
            }
        }
    }
    public void LoadScenes()
    {
        RemoveScenes();
        if (_manifest != null && _manifest.Scenes != null)
        {
            var bundleInfo = _manifest.Bundles.FirstOrDefault(bundle => bundle.Name.Contains("all_scenes"));
            if (bundleInfo != null)
            {
                var operation = ABLoader.LoadBundle(bundleInfo, _manifest);
                operation.Completed += LoadBundleScenes_Completed;
                StartCoroutine(operation);
            }
        }
    }

    private void LoadBundleScenes_Completed(ABAsyncOperationHandle<AssetBundle> obj)
    {
        _scenesBundle = obj.Result;
        if (obj.Result.isStreamedSceneAssetBundle)
        {
            foreach (var item in obj.Result.GetAllScenePaths())
            {
                Instantiate(_panelScenesPrefab, _containerScenes).GetComponent<PanelScene>().Initialize(item);
            }
        }
        else
        {
            foreach (var item in obj.Result.LoadAllAssets())
            {
                Instantiate(_panelAssetsPrefab, _containerAssets).GetComponent<PanelAsset>().Initialize(item);
            }
        }
    }

    private void LoadBundleAssets_Comleted(ABAsyncOperationHandle<AssetBundle> obj)
    {
        _assetsBundle = obj.Result;
        foreach (var item in obj.Result.LoadAllAssets())
        {
            Instantiate(_panelAssetsPrefab, _containerAssets).GetComponent<PanelAsset>().Initialize(item);
        }
    }

    private void LoadManifest_Completed(ABAsyncOperationHandle<ABManifest> obj)
    {
        _manifest = obj.Result;
        _textManifestName.text = _manifest.Name;
        _textManifestVersion.text = _manifest.Version;
        _textManifestRemote.text = _manifest.RemoteLoadPath;
        _textManifestLocal.text = _manifest.LocalLoadPath;
        RemoveAllChilds();
    }

    private void RemoveAllChilds()
    {
        RemoveAssets();
        RemoveScenes();
    }
    private void RemoveAssets()
    {
        if (_assetsBundle != null)
        {
            _assetsBundle.Unload(false);
        }
        RemoveContainerChilds(_containerAssets);
    }
    private void RemoveScenes()
    {
        if (_scenesBundle != null)
        {
            _scenesBundle.Unload(false);
        }
        RemoveContainerChilds(_containerScenes);
    }
    private void RemoveContainerChilds(Transform _container)
    {
        for (int i = 0; i < _container.childCount; i++)
        {
            var newChild = _container.GetChild(i);
            Destroy(newChild.gameObject);
        }
    }
}
