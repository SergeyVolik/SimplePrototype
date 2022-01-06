using UnityEngine;

namespace SerV112.UtilityAI.Game
{
	[CreateAssetMenu(fileName = "BulletPoolSO", menuName = "Pool/BulletPoolSO")]
	public class BulletPoolSO : ComponentPoolSO<PistolBullet>
	{
		[SerializeField]
		private InstantiateBulletPrefabFactory _factory;

		public override IFactory<PistolBullet> Factory
		{
			get
			{
				return _factory;
			}
			set
			{
				_factory = value as InstantiateBulletPrefabFactory;
			}
		}
	}


}
