using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ABManagerCore.Manifest;

namespace ABManagerRuntime.Managers
{
    public class TemplateManager : MonoBehaviour
    {
        [SerializeField]
        private ABManifest _manifest; 


        private void Awake()
        {
            DontDestroyOnLoad(this);
            string version = string.Empty;
            TasksManager.Current.AddTaskByAsyncOperation(
                UnityWebRequest.Get("http://localhost:5000/versions/current").SendWebRequest(),
                (progress) =>
                {
                    Debug.Log($"Progress: {progress}");
                })
                .Execute((operation) => 
                {
                    if (operation.isDone)
                    {
                        var request = operation.webRequest;
                        version = request.downloadHandler.text;
                    }
                    
                });
            TasksManager.Current.AddTaskByAsyncOperation(
                UnityWebRequest.Get($"http://localhost:5000/content/{version}/manifest").SendWebRequest(),
                (progress) =>
                {
                    Debug.Log($"Progress: {progress}");
                });
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetVersion();
            }
        }

        private void GetVersion()
        {
            if (_manifest != null)
            {
                Debug.Log(_manifest.Version);
            }
        }
    }
}

