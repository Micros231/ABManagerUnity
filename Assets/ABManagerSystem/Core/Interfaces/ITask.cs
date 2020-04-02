using System;
using UniRx.Async;

namespace ABManagerCore.Interfaces
{
    public enum TaskState
    {
        Executing,
        Create,
        Complete,
        Error
    }
    public interface ITask
    {
        UniTask UniTask { get; }
        bool IsDone { get; }
        TaskState TaskState { get; }
        void Execute(Action onCompleted);
    }
    public interface ITask<T> : ITask
    {
        T Response { get; }
        float Progress { get; }

        void Execute(Action<T> onCompleted);
    }
}

