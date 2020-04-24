using System.IO;
using UnityEditor;
using UnityEngine;
using ABManagerEditor.Settings;
using ABManagerCore.Helpers;
using ABManagerCore.Settings;
using System;
using ABManagerEditor.Helpers;

namespace ABManagerEditor.Controller
{
    internal abstract class ABControllerAbstract 
    {
        internal ManagerSettings ManagerSettings
        {
            get => GetSettings(ref _managerSettings, EditorFilePathHelper.ManagerSettingsFilePath);
        }
        internal HostSettings HostSettings
        {
            get => GetSettings(ref _hostSettings, EditorFilePathHelper.HostSettingsFilePath);
        }

        private ManagerSettings _managerSettings;
        private HostSettings _hostSettings;

        private T GetSettings<T>(ref T cachedSettings, string filePath) where T : ScriptableObject
        {
            if (cachedSettings != null)
            {
                return cachedSettings;
            }
            if (File.Exists(filePath))
            {
                cachedSettings = AssetDatabase.LoadAssetAtPath<T>(filePath);
            }
            if (cachedSettings == null)
                Debug.LogWarning($"Не найден файл {typeof(T).Name}, пожалуйста создайте его!");
            return cachedSettings;
        }
    }
}

