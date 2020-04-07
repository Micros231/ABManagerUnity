using System.IO;
using UnityEngine;
using ABManagerCore.Consts;
using ABManagerCore.Helpers;

namespace ABManagerEditor.Helpers
{
    public class EditorFilePathHelper : MonoBehaviour
    {
        public static string ManagerSettingsFilePath => Path.Combine(EditorDirectoryPathHelper.ManagerSettingsDirectoryPath, FileNames.ManagerSettingsFile);
        public static string HostSettingsFilePath => Path.Combine(EditorDirectoryPathHelper.HostSettingsDirectoryPath, FileNames.HostSettingsFile);
        public static string ResourcesHostSettingsPath => ResourcesFilePathHelper.ResourcesHostSettingsPath;
    }
}

