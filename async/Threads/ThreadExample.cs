using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadExample : MonoBehaviour {

    public int loops = 1000;

    private void Start() {
        launchThread();
        StartCoroutine(CoroutineTest());
    }
    public void threadTest() {
        for (int i = 0; i < loops; i++) {
            Debug.Log(i);
            Thread.Sleep(1);
        }
    }

    public void launchThread() {
        Thread t = new Thread(new ThreadStart(threadTest));
        t.Start();
    }

    public IEnumerator CoroutineTest() {
        for (int i = 0; i < loops; i++) {
            Debug.LogError(i);
            yield return null;
        }
        Debug.Log("Coroutine fin!");
    }
}
