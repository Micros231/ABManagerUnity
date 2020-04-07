using System;

namespace ABManagerCore.Interfaces
{
    public interface IABRequest<T>
    {
        IResponseOperation<T> SendRequest(Action<float> progressHandler);
    }
}

