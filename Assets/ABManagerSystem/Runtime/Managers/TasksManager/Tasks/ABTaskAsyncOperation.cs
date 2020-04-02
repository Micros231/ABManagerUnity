using ABManagerCore.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;

namespace ABManagerRuntime.Managers
{
    public class ABTaskAsyncOperation<T> : ABTask, ITask<T>
        where T : AsyncOperation
    {
        
        public T Response { get; private set; }

        public float Progress { get; private set; }

        public ABTaskAsyncOperation(UniTask task, T asyncOperation) : base(task)
        {
            Response = asyncOperation;
        }

        public void Execute(Action<T> onCompleted)
        {
            if (!IsDone && TaskState == TaskState.Create)
            {
                TaskState = TaskState.Executing;
                ExecuteTask(onCompleted).Forget();
            }
        }

        private async UniTaskVoid ExecuteTask(Action<T> onCompleted)
        {
            try
            {
                await UniTask;
                if (UniTask.IsCompleted)
                {
                    onCompleted?.Invoke(Response);
                    IsDone = true;
                    TaskState = TaskState.Complete;
                }
            }
            catch (Exception ex)
            {
                TaskState = TaskState.Error;
                Debug.LogError($"Error Executing: {ex.Message}");
            }
        }
    }
}

