using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game.Channels
{

	//[CreateAssetMenu(menuName = "Events/Request SFX Chanel")]
	public class SFXEventChannelSO : DescriptionBaseSO
	{
		public UnityAction<SFXSettingsSO, Vector3> OnLoadingRequested;

		public void RaiseEvent(SFXSettingsSO SFXSettins, Vector3 positon)
		{
			if (OnLoadingRequested != null)
			{
				OnLoadingRequested.Invoke(SFXSettins, positon);
			}
		}
	}

}

