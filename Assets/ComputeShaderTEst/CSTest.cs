using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSTest : MonoBehaviour
{
    ComputeShader m_Shader;

    const string curveData = "curveData";
    const string Result = "Result";
    const string TestComputeShader = "TestComputeShader";

    // Start is called before the first frame update
    void Start()
    {
        m_Shader = Resources.Load<ComputeShader>(TestComputeShader);



        float[] curve = { 1,2,3,4,5 };
        float[] curveout = new float[64];
        var buffer = new ComputeBuffer(curve.Length, sizeof(float));
        buffer.SetData(curve);
        var buffer2 = new ComputeBuffer(curveout.Length, sizeof(float));
        buffer2.SetData(curveout);

        m_Shader.SetBuffer(0, curveData, buffer);
        m_Shader.SetBuffer(0, Result, buffer2);

        int numberDropletsToSimulation = 10000;
        int numThreadGroups = numberDropletsToSimulation / 1024;


        m_Shader.Dispatch(0, 2, 1, 1);

        buffer2.GetData(curveout);


        for (int i = 0; i < curveout.Length; i++)
        {
            Debug.Log(curveout[i]);
        }

        buffer.Release();
        buffer2.Release();
    }


}
