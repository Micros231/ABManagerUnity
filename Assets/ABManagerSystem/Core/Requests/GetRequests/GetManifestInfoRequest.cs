using System;
using UnityEngine.Networking;
using ABManagerCore.Helpers;

namespace ABManagerCore.Requests.Get
{
    public class GetManifestInfoRequest : ABRequest<string>
    {
        public GetManifestInfoRequest(UnityWebRequest request) : base(request)
        {

        }
        public static GetManifestInfoRequest GetManifestInfoByVersion(string version)
        {
            var url = URLHelper.GetManifestInfoByVersionURL(version);
            if (CheckIsValidUrl(url))
            {
                var request = UnityWebRequest.Get(url);
                var getManifestInfoRequest = new GetManifestInfoRequest(request);
                return getManifestInfoRequest;
            }
            return null;
        }
        public static GetManifestInfoRequest GetCurrentManifestInfo()
        {
            var url = URLHelper.GetCurrentManifestInfoURL();
            if (CheckIsValidUrl(url))
            {
                var request = UnityWebRequest.Get(url);
                var getManifestInfoRequest = new GetManifestInfoRequest(request);
                return getManifestInfoRequest;
            }
            return null;
        }
        protected override void OnRequestError(bool isNetworkError, bool isHttpError, string errorMessage, Action<string> errorHandler)
        {
            errorHandler?.Invoke(errorMessage);
        }
        protected override void OnRequestSuccess(UnityWebRequest response, Action<string> responseHandler)
        {
            string manifestInfo = response.downloadHandler.text;
            responseHandler?.Invoke(manifestInfo);
        }
    }
}

