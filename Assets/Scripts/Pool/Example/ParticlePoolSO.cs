using UnityEngine;

namespace SerV112.UtilityAI.Game
{
	[CreateAssetMenu(fileName = "NewParticlePool", menuName = "Pool/Particle Pool")]
	public class ParticlePoolSO : ComponentPoolSO<ParticleSystem>
	{
		[SerializeField]
		private InstantiateParticleSystemPrefabFactory _factory;

		public override IFactory<ParticleSystem> Factory
		{
			get
			{
				return _factory;
			}
			set
			{
				_factory = value as InstantiateParticleSystemPrefabFactory;
			}
		}
	}


}
