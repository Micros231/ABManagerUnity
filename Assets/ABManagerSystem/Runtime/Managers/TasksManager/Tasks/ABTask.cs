using ABManagerCore.Interfaces;
using System;
using UniRx.Async;
using UnityEngine;

namespace ABManagerRuntime.Managers
{
    public class ABTask : ITask
    {
        public UniTask UniTask { get; protected set; }

        public bool IsDone { get; protected set; }

        public TaskState TaskState { get; protected set; }

        public ABTask(UniTask task)
        {
            TaskState = TaskState.Create;
            UniTask = task;
        }

        public void Execute(Action onCompleted)
        {
            if (!IsDone && TaskState == TaskState.Create)
            {
                TaskState = TaskState.Executing;
                ExecuteTask(onCompleted).Forget();
            }
            
        }

        private async UniTaskVoid ExecuteTask(Action onCompleted)
        {
            try
            {
                await UniTask;
                if (UniTask.IsCompleted)
                {
                    onCompleted?.Invoke();
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

