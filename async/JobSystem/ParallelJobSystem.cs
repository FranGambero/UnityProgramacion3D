using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class ParallelJobSystem : MonoBehaviour
{
    private float myNumber = 10f;
    private NativeArray<float> myData;
    private JobHandle myParallelJobHandle;

    private void freeMemoryData() {
        myData.Dispose();
    }

    private void initializeMemoryData() {
        myData = new NativeArray<float>(100, Allocator.TempJob);

        //fill the data in the array
        for (int i = 0; i < myData.Length; i++) {
            myData[i] = i;
        }
    }

    private void runDemo() {
        ParallelJob parallelJob = new ParallelJob {
            number = myNumber,
            data = myData
        };

        myParallelJobHandle = parallelJob.Schedule(myData.Length, 32);

        JobHandle.ScheduleBatchedJobs();

        myParallelJobHandle.Complete();

        if (myParallelJobHandle.IsCompleted) {
            for (int i = 0; i < myData.Length; i++) {
                Debug.Log(parallelJob.data[i]);
            }

            freeMemoryData();
        }

    }
}

public struct ParallelJob : IJobParallelFor {
    public float number;
    public NativeArray<float> data;

    public void Execute(int index) {
        data[index] += number;
    }
} 