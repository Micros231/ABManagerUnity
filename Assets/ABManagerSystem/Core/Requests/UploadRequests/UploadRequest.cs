using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace ABManagerCore.Requests.Upload
{
    public class UploadRequest : ABRequest<string>
    {
        protected UploadRequest(UnityWebRequest request) : base(request)
        {

        }
        public static UploadRequest UploadFile(string url, string fieldName, string path, string fileName, string mimeType)
        {
            if (CheckIsValidDataToUpload(url, fieldName, path, fileName, mimeType))
            {
                WWWForm form = new WWWForm();
                using (var fileStream = File.OpenRead(path))
                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    byte[] data = memoryStream.ToArray();
                    form.AddBinaryData(fieldName, data, fileName, mimeType);
                }

                var request = UnityWebRequest.Post(url, form);
                var uploadRequest = new UploadRequest(request);
                return uploadRequest;
            }
            return null;
        }
        private static bool CheckIsValidDataToUpload(string url, string filedName, string path, string fileName, string mimeType)
        {
            if (!CheckIsValidUrl(url))
            {
                return false;
            }
            if (string.IsNullOrEmpty(filedName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            if (path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                return false;
            }
            if (!File.Exists(path))
            {
                return false;
            }
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(mimeType))
            {
                return false;
            }
            if (!Regex.IsMatch(mimeType, "[a-z]/[a-z]"))
            {
                return false;
            }
            return true;
        }
        protected override void OnRequestError(bool isNetworkError, bool isHttpError, string errorMessage, Action<string> errorHandler)
        {
            errorHandler?.Invoke(errorMessage);
        }
        protected override void OnRequestSuccess(UnityWebRequest response, Action<string> responseHandler)
        {
            responseHandler?.Invoke("Upload Complete");
        }
    }
}

