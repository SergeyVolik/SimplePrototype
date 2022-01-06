using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public abstract class PlaySoundComponent<T> : MonoBehaviour where T : ISoundEvent
    {

        T Event;

        [SerializeField]
        protected SFXEvent m_SFX;

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            Event = GetComponent<T>();
        }
        private void OnEnable()
        {
            Event?.OnEvent.AddListener(PlaySFX);

        }
        private void OnDisable()
        {
            Event?.OnEvent.RemoveListener(PlaySFX);
        }

        protected virtual void PlaySFX()
        {
            OneShotAudioPool.Instance.PlaySFXWithPosition(m_SFX, transform.position);
        }
    }
}
