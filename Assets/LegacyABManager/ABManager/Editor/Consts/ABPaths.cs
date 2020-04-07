using System.Linq;
using UnityEditor;
using System.IO;

namespace ABManagerEditor.Consts
{
    public static class ABPaths
    {
        public static string MainDirecctoryPath => GetMainPath();
        public static string SettingsDirectoryPath => Path.Combine(MainDirecctoryPath, ABNames.Settings);
        public static string SettingsFilePath => Path.Combine(SettingsDirectoryPath, ABNames.FileSettings);
        public static string GroupsDirectoryPath => Path.Combine(MainDirecctoryPath, ABNames.Groups);
        private static string GetMainPath()
        {
            var paths = AssetDatabase.GetAllAssetPaths();
            var mainPath = paths.FirstOrDefault(path => path.Contains(ABNames.Manager));
            if (mainPath == null)
            {
                return string.Empty;
            }
            mainPath = mainPath.Remove(mainPath.IndexOf(ABNames.Manager) + ABNames.Manager.Count());
            return mainPath;
        }
    }
}

