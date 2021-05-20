using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// Taskの実験
/// </summary>
public class TaskTest : MonoBehaviour
{
    void Awake()
    {
        var T1 = Test1();
    }

    /// <summary>
    /// テストその１
    /// </summary>
    private async UniTask Test1()
    {
        Debug.Log("Start Test1");
        await UniTask.Delay(3000);
        Debug.Log("End Test1");
    }
}
