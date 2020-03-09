using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class SimpleJobSystem : MonoBehaviour
{
    // Float to adds in the myData
    private float myNumber = 5;

    //Simple native container of type float
    private NativeArray<float> myData;

    // Handle use to operate job in main thread
    private JobHandle simpleJobHandle;

    private void Start() {
        runDemo();
    }
    private void OnEnable() {
        initiliazeMemoryData();
    }

    private void runDemo() {
        // Simpl job declaration with the data
        SimpleJob simpleJob = new SimpleJob {
            number = myNumber,
            data = myData
        };

        //Schedule a simple job (added in the quee, not running)
        simpleJobHandle = simpleJob.Schedule();

        // run the schedule job
        JobHandle.ScheduleBatchedJobs();

        // wait for the job to be completed
        simpleJobHandle.Complete();

        if (simpleJobHandle.IsCompleted) {
            Debug.Log(simpleJob.data[0]);
        }

        // Free memory
        freeMemoryData();
    }


    private void initiliazeMemoryData() {
        myData = new NativeArray<float>(1, Allocator.Persistent);
        myData[0] = 2;
    }

    private void freeMemoryData() {
        myData.Dispose();
    }
}
