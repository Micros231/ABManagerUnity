using ABManagerCore.Consts;
using System.IO;

namespace ABManagerCore.Helpers
{
    public static class ResourcesDirectoryPathHelper
    {
        public static string ResourcesHostSettingsDirectoryPath => Path.Combine(DirectoryNames.HostSettings);

    }

}

