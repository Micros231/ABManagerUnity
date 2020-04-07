using UnityEngine;
using ABManagerCore.Interfaces;
using ABManagerCore.Helpers;
using UnityEditor;

namespace ABManagerCore.Settings
{
    public class HostSettings : ScriptableObject, IHostSettings
    {
        public static HostSettings Current => GetSettings();

        public string URLHost { get => _urlHost; internal set => _urlHost = value; }
        [SerializeField, HideInInspector]
        private string _urlHost;

        private static HostSettings GetSettings()
        {
            var settings = Resources.Load<HostSettings>(ResourcesFilePathHelper.ResourcesHostSettingsPath);
            return settings;
        }
    }
}

