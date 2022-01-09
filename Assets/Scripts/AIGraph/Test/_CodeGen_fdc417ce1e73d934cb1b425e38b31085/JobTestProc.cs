//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date:08.01.2022 23:04:53)
 //-----------------------------------------------------------------------
using System;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
using SerV112.UtilityAI.Math;
using SerV112.UtilityAI.Base;
[BurstCompile]
public struct JobTestProc : IUtilityAIJob<InAgentDataTestProc, OutAgentDataTestProc>
{
	[ReadOnly]
	private NativeArray<InAgentDataTestProc> InAgentDataTestProc;
	public NativeArray<InAgentDataTestProc> InAgentDataArray { get => InAgentDataTestProc; set => InAgentDataTestProc = value; }
	private NativeArray<OutAgentDataTestProc> OutAgentDataTestProc;
	public NativeArray<OutAgentDataTestProc> OutAgentDataArray { get => OutAgentDataTestProc; set => OutAgentDataTestProc = value; }

	public void Execute (int index)
	{
		var resultVar = new OutAgentDataTestProc();
		float max0 = UtilityAIMath.Max(0.88f,1.00f);
		max0 = UtilityAIMath.Max(max0, 0.42f);
		max0 = UtilityAIMath.Max(max0, 0.74f);
		max0 = UtilityAIMath.Max(max0, 0.52f);
		max0 = UtilityAIMath.Max(max0, 0.37f);
		max0 = UtilityAIMath.Max(max0, 0.63f);
		float oneminus0 = UtilityAIMath.OneMinus(max0);
		float min0 = UtilityAIMath.Min(0.88f,1.00f);
		min0 = UtilityAIMath.Min(min0, 0.42f);
		min0 = UtilityAIMath.Min(min0, 0.74f);
		min0 = UtilityAIMath.Min(min0, 0.52f);
		min0 = UtilityAIMath.Min(min0, 0.37f);
		min0 = UtilityAIMath.Min(min0, 0.63f);
		NativeArray<float> values0 = new  NativeArray<float>(2, Allocator.Temp);
		values0[0] = oneminus0;
		values0[1] = min0;
		Default selectIndex0 = (Default)UtilityAIMath.SelectAnswerIndex(values0);
		resultVar.Default = selectIndex0;

		OutAgentDataTestProc[index] = resultVar;
		values0.Dispose();
	}

}
[Serializable]
public struct InAgentDataTestProc {
}
[Serializable]
public struct OutAgentDataTestProc {
	public Default Default;
}

