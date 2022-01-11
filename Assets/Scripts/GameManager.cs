using SerV112.UtilityAI.Game.Channels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SerV112.UtilityAI.Game
{
    public class GameManager : UpdatableMonoBehaviour, IUpdate
    {
       

        private InputReader m_reader;
        [SerializeField]
        private GameObject PlayerDeadPanel;


        [SerializeField]
        private GameSceneSO m_Scene;
        [SerializeField]
        private LoadEventChannelSO m_ReloadScene;


        
        private Player m_Player;

        [SerializeField]
        private Cinemachine.CinemachineVirtualCamera m_Camera;


        [Inject]
        void Construct(Player player, InputReader playerInput)
        {
            m_Player = player;
            m_reader = playerInput;
        }

        private void Awake()
        {
           

            //m_Player = Instantiate(PlayerPrefab, m_PlayerSpawnPoint.position, Quaternion.identity);
            m_Camera.Follow = m_Player.transform;
            m_Camera.LookAt = m_Player.transform;

        }

        private void OnEnable()
        {
            SubscribeToUpdate(this);
            m_reader.EnableGameplayInput();
            m_reader.ShootingStartedEvent += M_reader_ShootEvent;
        }

        private void OnDisable()
        {
            UnsubscribeFromUpdate(this);
            m_reader.DisableGameplayInput();
            m_reader.ShootingStartedEvent -= M_reader_ShootEvent;
        }

        private void M_reader_ShootEvent()
        {
            if (m_Player == null)
            {
                m_ReloadScene.RaiseEvent(m_Scene);


            }
           
        }

        public void OnUpdate()
        {
            if (m_Player == null)
            {
                PlayerDeadPanel.SetActive(true);

            }
        }
    }

}
