using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UniRx;

/// <summary>
/// Taskの実験
/// </summary>
public class TaskTest : MonoBehaviour
{
    async void Awake()
    {
        await Test1();

        var TestTask = await Test2();
        Debug.Log(TestTask);

        var Test3Result = await Test3();
        Debug.Log(Test3Result);

        // awaitしない版
        var Test2Observable = Test3().ToObservable<string>();
        Test2Observable.Subscribe((Str) => Debug.Log("ToObservable: " + Str));

        Debug.Log("All Completed!");
    }

    /// <summary>
    /// テストその１
    /// 単純に３秒待つだけ
    /// </summary>
    private async UniTask Test1()
    {
        Debug.Log("Start Test1");
        await UniTask.Delay(3000);
        Debug.Log("End Test1");
    }

    /// <summary>
    /// テストその２
    /// ３秒待って文字列を返す
    /// </summary>
    /// <returns>文字列</returns>
    private async UniTask<string> Test2()
    {
        await UniTask.Delay(3000);
        return "End Task2";
    }

    /// <summary>
    /// テストその３
    /// コルーチンからの変換
    /// </summary>
    /// <returns>文字列</returns>
    private UniTask<string> Test3()
    {
        var Obs = Observable.FromCoroutine<string>((o) => Test3Coroutine(o));
        return Obs.ToUniTask<string>();
    }

    /// <summary>
    /// Test3用のコルーチン
    /// </summary>
    /// <param name="Observer">Observer</param>
    private IEnumerator Test3Coroutine(IObserver<string> Observer)
    {
        yield return new WaitForSeconds(3.0f);      // 通信待ちみたいな？
        Observer.OnNext("Test3Coroutine Completed!");
        Observer.OnCompleted();
    }
}
