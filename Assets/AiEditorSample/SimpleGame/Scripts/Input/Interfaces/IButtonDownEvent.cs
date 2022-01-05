using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
	public interface IButtonDownEvent
	{
		public UnityEvent PressDown { get; }
	}
}
