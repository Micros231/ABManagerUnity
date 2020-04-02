using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SandboxMain : MonoBehaviour
{
    public Button okButton;
    public Button cancelButton;
    CancellationTokenSource cts;

    private async void Start()
    {
       await UniTask.WhenAll(TestUni(), TestUniTask(), TestFrameUniTask());
    }


    private async UniTask TestUniTask()
    {
        Debug.Log("TestUnitTask1");
        await UniTask.Delay(2000);
        Debug.Log("TestUniTask2");

    }
    private async UniTask TestUni()
    {
        Debug.Log("TestUni1");
        await UniTask.Delay(2000);
        Debug.Log("TestUni2");
    }
    private async UniTask TestFrameUniTask()
    {
        while (true)
        {
            await UniTask.DelayFrame(60);
            Debug.Log("Update 60 frames");
        }
    }
}

