using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYNamespace;
using System;
using Unity.Collections;
using Unity.Jobs;

public class UtilityAIJobSystemBenchmark : MonoBehaviour
{
    System.Diagnostics.Stopwatch benchmark = new System.Diagnostics.Stopwatch();
    private NativeArray<InAgentData> InAgentData;
    private NativeArray<OutAgentData> OutAgentData;
    // Start is called before the first frame update

    [SerializeField]
    int size = 1000;
    ProcessAI job;
    void Start()
    {
        InAgentData = new NativeArray<InAgentData>(size, Allocator.Persistent);
        OutAgentData = new NativeArray<OutAgentData>(size, Allocator.Persistent);

        job = new ProcessAI
        {
            InAgentData = InAgentData,
            OutAgentData = OutAgentData
        };
    }

    private void OnDestroy()
    {
        InAgentData.Dispose();
        OutAgentData.Dispose();
    }
    // Update is called once per frame
    void Update()
    {
        benchmark.Restart();
        benchmark.Start();

       

        JobHandle handle = job.Schedule(InAgentData.Length, size / SystemInfo.processorCount);
        handle.Complete();


        benchmark.Stop();

        Debug.Log(benchmark.Elapsed.TotalMilliseconds);


    }
}
