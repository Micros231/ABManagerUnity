using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore.Interfaces
{
    public interface IResponseOperation<T>
    {
        T Response { get; }
        bool IsDone { get; }
        bool IsError { get; }
        float Progress { get; }
        string ErrorMessage { get; }

        event Action<T> OnCompleted;
        event Action<float> OnProgress;
        event Action<string> OnError;
    }
}

