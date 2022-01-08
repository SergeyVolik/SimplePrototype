////-----------------------------------------------------------------------
//// This file is AUTO-GENERATED.
//// Changes for this script by hand might be lost when auto-generation is run.
//// (Generated date:07.01.2022 20:14:11)
// //-----------------------------------------------------------------------
//using System;
//using Unity.Jobs;
//using Unity.Collections;
//using Unity.Burst;
//using SerV112.UtilityAI.Math;
//using SerV112.UtilityAI.Base;
//namespace MYNamespace
//{
//	[BurstCompile]
//	public struct TestJobSystem : IUtilityAIJob<InAgentData, OutAgentData>
//	{
//		private static readonly float[] customCurveArray0 = {
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.01f,
//			0.43f,
//			1.00f,
//		};
//		private NativeList<InAgentData> InAgentData;
//		public NativeList<InAgentData> InAgentDataArray { get; set; }
//		[ReadOnly]
//		private NativeList<OutAgentData> OutAgentData;
//		public NativeList<OutAgentData> OutAgentDataArray { get; set; }

//		public void Execute (int index)
//		{
//			var resultVar = new OutAgentData();
//			float nomalizedHelath = UtilityAIMath.Normalize01(InAgentData[index].Helath, 0, 7);
//			float oneminus0 = UtilityAIMath.OneMinus(nomalizedHelath);
//			float sineCounter0 = UtilityAIMath.SineCurve(oneminus0, 0.50f, 0.00f, 0.00f);
//			float nomalizedSeeEnemy = UtilityAIMath.Normalize01(InAgentData[index].SeeEnemy, 0, 1);
//			float nomalizedAmmo = UtilityAIMath.Normalize01(InAgentData[index].Ammo, 0, 7);
//			float oneminus1 = UtilityAIMath.OneMinus(nomalizedAmmo);
//			float nomalizedHasGun = UtilityAIMath.Normalize01(InAgentData[index].HasGun, 0, 1);
//			NativeArray<float> floatArray0 = new  NativeArray<float>(3, Allocator.Temp);
//			floatArray0[0] = nomalizedSeeEnemy;
//			floatArray0[1] = customCurveArray0[(int)(oneminus1*100)];
//			floatArray0[2] = nomalizedHasGun;
//			float multiply0 = UtilityAIMath.Multiply(floatArray0);
//			NativeArray<float> floatArray1 = new  NativeArray<float>(3, Allocator.Temp);
//			floatArray1[0] = nomalizedSeeEnemy;
//			floatArray1[1] = nomalizedAmmo;
//			floatArray1[2] = nomalizedHasGun;
//			float multiply1 = UtilityAIMath.Multiply(floatArray1);
//			float oneminus2 = UtilityAIMath.OneMinus(nomalizedHasGun);
//			float exponential0 = UtilityAIMath.ExponentialCurve(nomalizedHelath, 100.00f, 0.00f);
//			NativeArray<float> floatArray2 = new  NativeArray<float>(2, Allocator.Temp);
//			floatArray2[0] = oneminus2;
//			floatArray2[1] = exponential0;
//			float multiply2 = UtilityAIMath.Multiply(floatArray2);
//			float oneminus3 = UtilityAIMath.OneMinus(nomalizedSeeEnemy);
//			NativeArray<float> floatArray3 = new  NativeArray<float>(3, Allocator.Temp);
//			floatArray3[0] = exponential0;
//			floatArray3[1] = oneminus3;
//			floatArray3[2] = nomalizedHasGun;
//			float multiply3 = UtilityAIMath.Multiply(floatArray3);
//			NativeArray<float> values0 = new  NativeArray<float>(5, Allocator.Temp);
//			values0[0] = multiply0;
//			values0[1] = multiply1;
//			values0[2] = multiply2;
//			values0[3] = multiply3;
//			values0[4] = sineCounter0;
//			int selectIndex0 = UtilityAIMath.SelectAnswerIndex(values0);
//			resultVar.SimpleAiActions = selectIndex0;

//			OutAgentData[index] = resultVar;
//			floatArray0.Dispose();
//			floatArray1.Dispose();
//			floatArray2.Dispose();
//			floatArray3.Dispose();
//			values0.Dispose();
//		}

//	}
//	[Serializable]
//	public struct InAgentData {
//		public float Helath;
//		public float SeeEnemy;
//		public float Ammo;
//		public float HasGun;
//	}
//	[Serializable]
//	public struct OutAgentData {
//		public int SimpleAiActions;
//	}

//}
