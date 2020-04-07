using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerRuntime.Loader
{
    public class ResponseHandler<T>
    {
        public T Response { get; private set; }
        public bool IsDone { get; private set; }
        public float Progress { get; private set; }

        public event Action<T> OnCompleted;

        internal Action<T> ResponceHandler { get; private set; }
        internal Action<float> ProgressHandler { get; private set; }

        public ResponseHandler (Action<float> progressHandler)
        {
            ProgressHandler = progressHandler;
            ResponceHandler += ExecuteReponceHandler;
            ProgressHandler += ExecuteProgressHandler;
        }


        private void ExecuteReponceHandler(T responce)
        {
            ResponceHandler -= ExecuteReponceHandler;
            Response = responce;
            OnCompleted?.Invoke(Response);
            IsDone = true;
            ProgressHandler -= ExecuteProgressHandler;
        }
        private void ExecuteProgressHandler(float progress)
        {
            Progress = progress;
        }
    }
}

