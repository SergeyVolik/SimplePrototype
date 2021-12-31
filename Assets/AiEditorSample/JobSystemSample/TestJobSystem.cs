//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date:27.12.2021 10:00:41)
 //-----------------------------------------------------------------------
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
using SerV112.UtilityAI.Math;
namespace MYNamespace
{
	[BurstCompile]
	public struct ProcessAI : IJobParallelFor
	{
		[ReadOnly]
		public NativeArray<InAgentData> InAgentData;
		public NativeArray<OutAgentData> OutAgentData;

		public void Execute (int index)
		{
			var resultVar = new OutAgentData();
			float nomalizedNormalizedValue = UtilityAIMath.Normalize01(InAgentData[index].NormalizedValue, 0, 100);
			NativeArray<float> values0 = new  NativeArray<float>(2, Allocator.Temp);
			values0[0] = nomalizedNormalizedValue;
			values0[1] = nomalizedNormalizedValue;
			float average0 = UtilityAIMath.Average(values0);
			float nomalizedNormalizedValue0 = UtilityAIMath.Normalize01(InAgentData[index].NormalizedValue0, 0, 50);
			float cosine0 = UtilityAIMath.CosineCurve(nomalizedNormalizedValue0, 1.00f, -0.50f, 0.00f);
			float exponential0 = UtilityAIMath.ExponentialCurve(cosine0, 1.00f, 0.00f);
			float sineCounter0 = UtilityAIMath.SineCurve(exponential0, 1.00f, -0.06f, 0.11f);
			float linear0 = UtilityAIMath.LinearCurve(sineCounter0, 1.00f, 0.00f);
			float logistic0 = UtilityAIMath.LogisticCurve(linear0, 1.00f, 0.00f);
			float smoothstepCounter0 = UtilityAIMath.SmoothstepCurve(logistic0);
			float smootherstepCounter0 = UtilityAIMath.SmootherstepCurve(smoothstepCounter0);
			float cosine1 = UtilityAIMath.CosineCurve(smootherstepCounter0, 1.00f, -0.50f, 0.00f);
			float logit0 = UtilityAIMath.LogitCurve(cosine1, 2.50f);
			float oneminus0 = UtilityAIMath.OneMinus(logit0);
			NativeArray<float> values1 = new  NativeArray<float>(2, Allocator.Temp);
			values1[0] = average0;
			values1[1] = oneminus0;
			int selectIndex0 = UtilityAIMath.SelectAnswerIndex(values1);
			resultVar.Default = selectIndex0;

			OutAgentData[index] = resultVar;
			values0.Dispose();
			values1.Dispose();
		}

	}
	public struct InAgentData {
		public float NormalizedValue;
		public float NormalizedValue0;
	}
	public struct OutAgentData {
		public int Default;
	}

}
