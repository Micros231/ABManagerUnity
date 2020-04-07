using System.IO;
using ABManagerCore.Consts;

namespace ABManagerCore.Helpers
{
    public static class ResourcesFilePathHelper
    {
        public static string ResourcesHostSettingsPath => Path.Combine(ResourcesDirectoryPathHelper.ResourcesHostSettingsDirectoryPath, Names.HostSettings);
    }
}

