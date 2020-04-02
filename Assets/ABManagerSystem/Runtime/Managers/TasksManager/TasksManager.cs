using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;
using UnityEngine.Networking;
using ABManagerCore.Interfaces;
using System.IO;

namespace ABManagerRuntime.Managers
{
    public class TasksManager : MonoBehaviour
    {
        public static TasksManager Current => instance;
        private static TasksManager instance = null;

        public IReadOnlyList<ITask> Tasks => _tasks;

        [SerializeField]
        private List<ITask> _tasks = new List<ITask>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        private void Start()
        {
            UpdateTasks().Forget();
        }
        public async UniTaskVoid UpdateTasks()
        {
            while (true)
            {
                if (_tasks != null && _tasks.Count > 0)
                {
                    foreach (var task in _tasks)
                    {
                        task.Execute(() => Debug.Log("complete"));
                    }
                }
                await UniTask.DelayFrame(1);
            }
        }
        public ITask AddTask(UniTask task)
        {
            ITask newTask = new ABTask(task);
            _tasks.Add(newTask);
            return newTask;
        }
        public ITask<T> AddTaskByAsyncOperation<T>(T asyncOperation, Action<float> progressHandler = null)
            where T : AsyncOperation
        {
            UniTask uniTask;
            if (progressHandler != null)
            {
                var progress = Progress.Create(progressHandler);
                uniTask = asyncOperation.ConfigureAwait(progress: progress);
            }
            else
            {
                uniTask = asyncOperation.ToUniTask();
            }
            var task = new ABTaskAsyncOperation<T>(uniTask, asyncOperation);
            _tasks.Add(task);
            return task;
        }
    }
}

