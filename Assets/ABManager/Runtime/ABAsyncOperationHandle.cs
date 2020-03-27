using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace ABManagerRuntime
{
    public struct ABAsyncOperationHandle<TObject> : IEnumerator
    {
        public object Current => Result;
        public Task<TObject> Task => GetResult();
        public bool IsDone { get; private set; }
        public bool IsValid { get; }
        public float Progress { get; private set; }
        public TObject Result { get; private set; }

        public event Action<ABAsyncOperationHandle<TObject>> Completed;

        private readonly UnityWebRequestAsyncOperation _asyncOperation;
        private readonly ABDownloadHandlerBase<TObject> _downloadHandler;

        public ABAsyncOperationHandle(UnityWebRequest request, ABDownloadHandlerBase<TObject> downloadHandler) : this()
        {
            _asyncOperation = request.SendWebRequest();
            _downloadHandler = downloadHandler;
            IsValid = true;
        }
        public bool MoveNext()
        {
            if (!IsValid)
            {
                return IsValid;
            }
            if (IsDone)
            {
                return false;
            }
            Progress = _asyncOperation.progress;
            if (_asyncOperation.isDone)
            {
                var request = _asyncOperation.webRequest;
                Result = _downloadHandler.GetContent(request);
                IsDone = true;
                Completed?.Invoke(this);
                return false;
            }
            return !IsDone;

        }
        public void Reset()
        {

        }

        private Task<TObject> GetResult()
        {
            while (!IsDone && IsValid)
            {
                Progress = _asyncOperation.progress;
                if (_asyncOperation.isDone)
                {
                    var request = _asyncOperation.webRequest;
                    Result = _downloadHandler.GetContent(request);
                    IsDone = true;
                    Completed?.Invoke(this);
                }
            }
            return Task<TObject>.FromResult(Result);
        }

    }
}

