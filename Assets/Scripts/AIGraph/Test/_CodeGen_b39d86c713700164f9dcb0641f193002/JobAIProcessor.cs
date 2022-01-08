//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date:08.01.2022 9:18:45)
 //-----------------------------------------------------------------------
using System;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
using SerV112.UtilityAI.Math;
using SerV112.UtilityAI.Base;
[BurstCompile]
public struct JobAIProcessor : IUtilityAIJob<InAgentDataAIProcessor, OutAgentDataAIProcessor>
{
	[ReadOnly]
	private NativeArray<InAgentDataAIProcessor> InAgentDataAIProcessor;
	public NativeArray<InAgentDataAIProcessor> InAgentDataArray { get => InAgentDataAIProcessor; set => InAgentDataAIProcessor = value; }
	private NativeArray<OutAgentDataAIProcessor> OutAgentDataAIProcessor;
	public NativeArray<OutAgentDataAIProcessor> OutAgentDataArray { get => OutAgentDataAIProcessor; set => OutAgentDataAIProcessor = value; }

	public void Execute (int index)
	{
		var resultVar = new OutAgentDataAIProcessor();
		float nomalizedNormalizedValue = UtilityAIMath.Normalize01(InAgentDataAIProcessor[index].NormalizedValue, 0, 1);
		float oneminus0 = UtilityAIMath.OneMinus(nomalizedNormalizedValue);
		NativeArray<float> values0 = new  NativeArray<float>(2, Allocator.Temp);
		values0[0] = nomalizedNormalizedValue;
		values0[1] = oneminus0;
		Default selectIndex0 = (Default)UtilityAIMath.SelectAnswerIndex(values0);
		resultVar.Default = selectIndex0;

		OutAgentDataAIProcessor[index] = resultVar;
		values0.Dispose();
	}

}
[Serializable]
public struct InAgentDataAIProcessor {
	public float NormalizedValue;
}
[Serializable]
public struct OutAgentDataAIProcessor {
	public Default Default;
}

