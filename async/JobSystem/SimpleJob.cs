using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public struct SimpleJob : IJob
{
    public float number;
    public NativeArray<float> data;

    public void Execute() {
        data[0] += number;
    }
}
