using UnityEngine.Networking;
using ABManagerCore.Helpers;
using ABManagerCore.Consts;

namespace ABManagerCore.Requests.Upload
{
    public class UploadManifestRequest : UploadRequest
    {
        protected UploadManifestRequest(UnityWebRequest request) : base(request)
        {
            
        }
        public static UploadManifestRequest UploadManifest(string path)
        {
            var uploadFileRequest = UploadFile(URLHelper.UploadManifestURL(), FormFieldNames.ManifestField, path, FileNames.ManifestFile, MimeTypes.JSON);
            var request = uploadFileRequest.Request;
            return new UploadManifestRequest(request);
        }
    }
}

