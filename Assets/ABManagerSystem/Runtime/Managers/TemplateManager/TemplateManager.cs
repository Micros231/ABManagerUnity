using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ABManagerCore.Manifest;
using ABManagerCore.Requests;

namespace ABManagerRuntime.Managers
{
    public class TemplateManager : MonoBehaviour
    {
        [SerializeField]
        private ABManifest _manifest;

        private void Awake()
        {
            DontDestroyOnLoad(this);
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

