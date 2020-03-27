using System.IO;
using UnityEngine;
using UnityEditor;
using ABManagerEditor.Models;
using ABManagerEditor.Consts;

namespace ABManagerEditor.Controller.Creators 
{
    internal class ABMSettingsCreator : ABManagerAbstract
    {
        internal ABSettings Create()
        {
            if (Settings != null)
            {
                return Settings;
            }
            else
            {
                ABSettings settings = ScriptableObject.CreateInstance<ABSettings>();
                if (!Directory.Exists(ABPaths.SettingsDirectoryPath))
                {
                    AssetDatabase.CreateFolder(ABPaths.MainDirecctoryPath, ABNames.Settings);
                }
                AssetDatabase.CreateAsset(settings, ABPaths.SettingsFilePath);
                
                return InitialSetup(settings);
            }
        }
        protected internal ABSettings InitialSetup(ABSettings createdObj)
        {
            if (createdObj == null)
                throw new System.NullReferenceException("Settings is null");
            createdObj.Version = "1";
            createdObj.BuildPath = $"{Application.dataPath}/{ABNames.Data}/Builds";
            createdObj.LocalLoadPath = $"{Application.persistentDataPath}/{ABNames.Data}";
            createdObj.RemoteLoadPath = $"http://localhost/{ABNames.Data}";
            return createdObj;
        }
    }
}

