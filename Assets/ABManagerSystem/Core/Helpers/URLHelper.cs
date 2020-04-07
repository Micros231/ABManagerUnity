using ABManagerCore.Settings;
using ABManagerCore.Consts;

namespace ABManagerCore.Helpers
{
    public static class URLHelper
    {
        public static string URLHost => HostSettings.Current.URLHost;

        public static string GetCurrentVersionURL()
        {
            var absoluteUrl = URLHost + RelativeURLs.GetCurrentVersion;
            return absoluteUrl;
        }
        public static string GetManifestInfoByVersionURL(string version)
        {
            var absoluteUrl = URLHost + string.Format(RelativeURLs.GetManifestInfoByVersionFormat, version);
            return absoluteUrl;
        }
        public static string GetCurrentManifestInfoURL()
        {
            var absoluteUrl = URLHost + RelativeURLs.GetCurrentManifestInfo;
            return absoluteUrl;
        }
        public static string DownloadManifestByVersionURL(string version)
        {
            var absoluteUrl = URLHost + string.Format(RelativeURLs.DownloadManifestByVersionFormat, version);
            return absoluteUrl;
        }
        public static string DownloadCurrentManifestURL()
        {
            var absoluteUrl = URLHost + RelativeURLs.DownloadCurrentManifest;
            return absoluteUrl;
        }
        public static string UploadManifestURL()
        {
            var absoluteUrl = URLHost + RelativeURLs.UploadManifest;
            return absoluteUrl;
        }
    }
}

