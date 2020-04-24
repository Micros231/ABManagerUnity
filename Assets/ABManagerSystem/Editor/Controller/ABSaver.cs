using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.Controller
{
    internal class ABSaver : ABControllerAbstract
    {

        internal void SaveAllSettings()
        {
            SaveManagerSettings();
            SaveHostSettings();
        }
        internal void SaveManagerSettings()
        {
            if (!CheckSettingsIsNull(ManagerSettings))
            {
                /*foreach (var group in Settings.Items)
                {
                SaveGroup(group);
                }*/
                EditorUtility.SetDirty(ManagerSettings);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
            
        }
        internal void SaveHostSettings()
        {
            if (!CheckSettingsIsNull(HostSettings))
            {
                EditorUtility.SetDirty(HostSettings);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
        }
        private bool CheckSettingsIsNull<T>(T settings) where T: ScriptableObject
        {
            if (settings == null)
            {
                Debug.LogWarning($"Сохранение {typeof(T).Name} не произошло. {typeof(T).Name} is null");
                throw new NullReferenceException($"{typeof(T).Name} is null");
            }
            return false;
        }
    }
}

