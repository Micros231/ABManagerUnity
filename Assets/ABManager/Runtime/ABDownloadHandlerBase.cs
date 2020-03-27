using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ABManagerRuntime
{
    public abstract class ABDownloadHandlerBase<TResult>
    {
        public TResult Result { get; protected set; }
        public abstract TResult GetContent(UnityWebRequest request);
    }
}

