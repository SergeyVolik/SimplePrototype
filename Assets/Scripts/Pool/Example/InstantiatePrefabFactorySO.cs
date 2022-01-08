using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SerV112.UtilityAI.Game
{
	public abstract class InstantiatePrefabFactorySO<T> : FactorySO<T> where T : Component
	{
		[SerializeField]
		private T m_Prefab;
		public override T Create()
		{
			return Instantiate(m_Prefab);
		}
	}

}

