using System;
using UnityEngine.Networking;
using ABManagerCore.Helpers;

namespace ABManagerCore.Requests.Get
{
    public class GetCurrentVersionRequest : ABRequest<string>
    {

        protected GetCurrentVersionRequest(UnityWebRequest request) : base(request)
        {

        }
        public static GetCurrentVersionRequest GetVersion()
        {
            var url = URLHelper.GetCurrentVersionURL();
            if (CheckIsValidUrl(url))
            {
                var request = UnityWebRequest.Get(url);
                var getVersionRequest = new GetCurrentVersionRequest(request);
                return getVersionRequest;
            }
            return null;
        }

        protected override void OnRequestError(bool isNetworkError, bool isHttpError, string errorMessage, Action<string> errorHandler)
        {
            errorHandler?.Invoke(errorMessage);
        }

        protected override void OnRequestSuccess(UnityWebRequest response, Action<string> responseHandler)
        {
            responseHandler?.Invoke(response.downloadHandler.text);
        }
    }
}

