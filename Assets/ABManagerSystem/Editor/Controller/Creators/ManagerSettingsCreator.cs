using ABManagerEditor.Settings;
using ABManagerCore.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using ABManagerEditor.Helpers;

namespace ABManagerEditor.Controller.Creators
{
    internal class ManagerSettingsCreator : ABControllerAbstract
    {
        internal ManagerSettings Create()
        {
            if (ManagerSettings != null)
            {
                return ManagerSettings;
            }
            ManagerSettings settings = ScriptableObject.CreateInstance<ManagerSettings>();
            if (!Directory.Exists(EditorDirectoryPathHelper.ManagerSettingsDirectoryPath))
            {
                Directory.CreateDirectory(EditorDirectoryPathHelper.ManagerSettingsDirectoryPath);
            }
            AssetDatabase.CreateAsset(settings, EditorFilePathHelper.ManagerSettingsFilePath);
            return InitialSetup(settings);
        }

        private  ManagerSettings InitialSetup(ManagerSettings createdObj)
        {
            if (createdObj == null)
                throw new System.NullReferenceException("Settings is null");
            createdObj.Version = "1";
            createdObj.BuildPath = EditorDirectoryPathHelper.DataDirectoryPath;
            createdObj.BuildTarget = BuildTarget.StandaloneWindows;
            return createdObj;
        }
    }
}

