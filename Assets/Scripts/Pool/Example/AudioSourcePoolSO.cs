using UnityEngine;

namespace SerV112.UtilityAI.Game
{
	[CreateAssetMenu(fileName = "AudioSourcePoolSO", menuName = "Pool/AudioSourcePoolSO")]
	public class AudioSourcePoolSO : ComponentPoolSO<AudioSource>
	{
		[SerializeField]
		private InstantiateAudioSourcePrefabFactory _factory;

		public override IFactory<AudioSource> Factory
		{
			get
			{
				return _factory;
			}
			set
			{
				_factory = value as InstantiateAudioSourcePrefabFactory;
			}
		}

		public override AudioSource Request()
		{
			AudioSource member = base.Request();
			member.gameObject.SetActive(true);
			return member;
		}

		public override void Return(AudioSource member)
		{
			member.transform.SetParent(PoolRoot.transform);
			member.gameObject.SetActive(false);
			base.Return(member);
		}

	}


}
