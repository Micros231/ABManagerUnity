using ABManagerCore.Manifest;
using System;
using UniRx.Async;
using UnityEngine.Networking;
using ABManagerCore.Interfaces;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ABManagerCore.Requests
{
    public abstract class ABRequest<T> : IABRequest<T>, IDisposable
    {
        protected internal UnityWebRequest Request { get; }
        protected ABRequest(UnityWebRequest request)
        {
            Request = request;
        }
        public IResponseOperation<T> SendRequest(Action<float> progressHandler = null)
        {
            var operation = ResponseOperation<T>.Create(progressHandler: progressHandler);
            var progress = Progress.Create(operation.ProgressHandler);
#if UNITY_EDITOR
            var task = Request.SendWebRequest().ToUniTask();
#else
            var task = Request.SendWebRequest().ConfigureAwait(progress: progress);
#endif
            ExecuteRequest(task, operation.ResponseHandler, operation.ErrorHandler).Forget();
            return operation;
        }
        public virtual void Dispose()
        {
            Request.Dispose();
        }
        protected async UniTaskVoid ExecuteRequest(UniTask<UnityWebRequest> task, Action<T> responseHandler, Action<string> errorHandler)
        {
            await task;
            if (task.IsCompleted)
            {
                var response = task.Result;
                if (response.isHttpError || response.isNetworkError)
                {
                    OnRequestError(response.isNetworkError, response.isHttpError, response.error, errorHandler);
                }
                else if (response.isDone && (!response.isHttpError || !response.isNetworkError))
                {
                    OnRequestSuccess(response, responseHandler);
                }
            }
            Request.Abort();
            Request.Dispose();
        }
        protected abstract void OnRequestError(bool isNetworkError, bool isHttpError, string errorMessage, Action<string> errorHandler);
        protected abstract void OnRequestSuccess(UnityWebRequest response, Action<T> responseHandler);
        protected static bool CheckIsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return false;
            }
            return true;
        }

        
    }
}

