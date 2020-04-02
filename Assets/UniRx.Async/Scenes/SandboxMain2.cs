using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;

public class SandboxMain2 : MonoBehaviour
{
    private void Start()
    {
        UniTask.WhenAll(TestUni(), TestUniTask(), TestFrameUniTask()).Forget();
    }


    private async UniTask TestUniTask()
    {
        Debug.Log("SandboxMain2TestUnitTask1");
        await UniTask.Delay(2000);
        Debug.Log("SandboxMain2TestUniTask2");

    }
    private async UniTask TestUni()
    {
        Debug.Log("SandboxMain2TestUni1");
        await UniTask.Delay(2000);
        Debug.Log("SandboxMain2TestUni2");
    }
    private async UniTask TestFrameUniTask()
    {
        while (true)
        {
            await UniTask.DelayFrame(60);
            Debug.Log("SandboxMain2Update 60 frames");
        }
    }
}
