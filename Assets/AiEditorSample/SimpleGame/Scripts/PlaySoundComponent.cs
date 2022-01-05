using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public abstract class PlaySoundComponent<T> : MonoBehaviour where T : ISoundEvent
    {

        T Event;
        [SerializeField]
        protected AudioClip m_Clip;
        [SerializeField, Range(0, 1)]
        protected float m_Volume = 1;

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
            OneShotAudioPool.Instance.PlayClipAtPoint(m_Clip, transform.position, m_Volume);
        }
    }
}
