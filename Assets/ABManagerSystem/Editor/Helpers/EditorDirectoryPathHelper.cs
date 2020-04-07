using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using ABManagerCore.Consts;
using ABManagerCore.Helpers;

namespace ABManagerEditor.Helpers
{
    public static class EditorDirectoryPathHelper
    {
        public static string MainDirectoryPath => GetMainPath();
        public static string ManagerSettingsDirectoryPath => Path.Combine(MainDirectoryPath, DirectoryNames.Editor, DirectoryNames.ManagerSettings);
        public static string HostSettingsDirectoryPath => Path.Combine(MainDirectoryPath, DirectoryNames.Runtime, DirectoryNames.Resources, ResourcesDirectoryPathHelper.ResourcesHostSettingsDirectoryPath);
        public static string DataDirectoryPath => Path.Combine(MainDirectoryPath, DirectoryNames.Data);
        private static string GetMainPath()
        {
            var paths = AssetDatabase.GetAllAssetPaths();
            var mainPath = paths.FirstOrDefault(path => path.Contains(DirectoryNames.ABManagerSystem));
            if (string.IsNullOrEmpty(mainPath))
            {
                Debug.Log($"Не найден путь до папки {DirectoryNames.ABManagerSystem}");
                return string.Empty;
            }
            mainPath = mainPath.Remove(mainPath.IndexOf(DirectoryNames.ABManagerSystem) + DirectoryNames.ABManagerSystem.Count());
            return mainPath;
        }
    }
}

