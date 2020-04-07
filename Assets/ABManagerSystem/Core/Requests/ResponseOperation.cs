using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Interfaces;
using System;

namespace ABManagerCore.Requests
{

    public class ResponseOperation<T> : IResponseOperation<T>
    {
        public event Action<T> OnCompleted
        {
            add
            {
                if (IsDone && !IsError)
                {
                    value(Response);
                }
                else
                {
                    _completeCallback += value;
                }
            }
            remove
            {
                _completeCallback -= value;
            }
        }
        public event Action<float> OnProgress
        {
            add
            {
                if (IsDone)
                {
                    value(Progress);
                }
                else
                {
                    _progressCallback += value;
                }
            }
            remove
            {
                _progressCallback -= value;
            }
        }
        public event Action<string> OnError
        {
            add
            {
                if (IsDone && IsError)
                {
                    value(ErrorMessage);
                }
                else
                {
                    _errorCallback += value;
                }
            }
            remove
            {
                _errorCallback -= value;
            }
        }

        public T Response { get; private set; }
        public bool IsDone { get; private set; }
        public bool IsError { get; private set; }
        public float Progress { get; private set; }
        public string ErrorMessage { get; private set; }

        internal Action<T> ResponseHandler { get; private set; }
        internal Action<float> ProgressHandler { get; private set; }
        internal Action<string> ErrorHandler { get; private set; }

        private Action<T> _completeCallback { get; set; }
        private Action<float> _progressCallback { get; set; }
        private Action<string> _errorCallback { get; set; }

        private ResponseOperation()
        {
            ErrorMessage = string.Empty;
            ResponseHandler += InvokeReponceHandler;
            ProgressHandler += InvokeProgressHandler;
            ErrorHandler += InvokeErrorHandler;
        }

        private void InvokeReponceHandler(T responce)
        {
            Response = responce;
            IsDone = true;
            Progress = 1f;
            _progressCallback?.Invoke(Progress);
            _completeCallback?.Invoke(Response);
            ClearCallbacks();
        }
        private void InvokeProgressHandler(float progress)
        {
            Progress = progress;
            _progressCallback?.Invoke(Progress);
        }
        private void InvokeErrorHandler(string errorMessage)
        {
            IsError = true;
            ErrorMessage = errorMessage;
            Progress = 1f;
            _progressCallback?.Invoke(Progress);
            _errorCallback?.Invoke(ErrorMessage);
            ClearCallbacks();
        }
        private void ClearCallbacks()
        {
            ResponseHandler = null;
            ProgressHandler = null;
            ErrorHandler = null;
            _completeCallback = null;
            _progressCallback = null;
            _errorCallback = null;
        }
        internal static ResponseOperation<T> Create(Action<T> responseHandler = null, Action<string> errorHandler = null, Action<float> progressHandler = null)
        {
            var responce = new ResponseOperation<T>();
            responce.ResponseHandler += responseHandler;
            responce.ProgressHandler += progressHandler;
            responce.ErrorHandler += errorHandler;
            return responce;
        }
    }
}

