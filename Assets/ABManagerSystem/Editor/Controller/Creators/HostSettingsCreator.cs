using ABManagerCore.Helpers;
using ABManagerCore.Settings;
using ABManagerEditor.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.Controller.Creators
{
    internal class HostSettingsCreator : ABControllerAbstract
    {
        internal HostSettings Create()
        {
            if (HostSettings != null)
            {
                return HostSettings;
            }
            HostSettings settings = ScriptableObject.CreateInstance<HostSettings>();
            if (!Directory.Exists(EditorDirectoryPathHelper.HostSettingsDirectoryPath))
            {
                Directory.CreateDirectory(EditorDirectoryPathHelper.HostSettingsDirectoryPath);
            }
            AssetDatabase.CreateAsset(settings, EditorFilePathHelper.HostSettingsFilePath);
            return InitialSetup(settings);
        }

        private HostSettings InitialSetup(HostSettings createdObj)
        {
            if (createdObj == null)
                throw new System.NullReferenceException("Settings is null");
            createdObj.URLHost = "http://localhost:5000";
            return createdObj;
        }
    }
}

