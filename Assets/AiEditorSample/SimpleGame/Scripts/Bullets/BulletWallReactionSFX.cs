using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PistolBullet))]
    public class BulletWallReactionSFX : MonoBehaviour
    {
        [SerializeField]
        private AudioClip clip;
        private PistolBullet PistolBullet;
        // Start is called before the first frame update
        void Awake()
        {
            PistolBullet = GetComponent<PistolBullet>();
        }

        private void OnEnable()
        {
            PistolBullet.OnHit.AddListener(Play);
        }

        private void OnDisable()
        {
            
        }

        void Play(IDamageApplicator dam)
        {
            if (dam == null)
            {
                OneShotAudioPool.Instance.PlayClipAtPoint(clip, transform.position, 0.3f);
            }
        }

        
    }

}

