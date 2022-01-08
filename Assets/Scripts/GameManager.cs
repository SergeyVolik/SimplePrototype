using SerV112.UtilityAI.Game.Channels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SerV112.UtilityAI.Game
{
    public class GameManager : MonoBehaviour
    {
       
        [SerializeField]
        InputReader m_reader;
        [SerializeField]
        private GameObject PlayerDeadPanel;

        [SerializeField]
        private GameObject PlayerPrefab;

        [SerializeField]
        private Transform m_PlayerSpawnPoint;

        [SerializeField]
        private GameSceneSO m_Scene;
        [SerializeField]
        private LoadEventChannelSO m_ReloadScene;

        private GameObject m_Player;

        [SerializeField]
        private Cinemachine.CinemachineVirtualCamera m_Camera;
        private void Awake()
        {
           

            m_Player = Instantiate(PlayerPrefab, m_PlayerSpawnPoint.position, Quaternion.identity);
            m_Camera.Follow = m_Player.transform;
            m_Camera.LookAt = m_Player.transform;

        }

        private void OnEnable()
        {
           
            m_reader.EnableGameplayInput();
            m_reader.ShootingStartedEvent += M_reader_ShootEvent;
        }

        private void OnDisable()
        {
            m_reader.DisableGameplayInput();
            m_reader.ShootingStartedEvent -= M_reader_ShootEvent;
        }

        private void Update()
        {
            if (m_Player == null)
            {
                PlayerDeadPanel.SetActive(true);

            }
        }

        private void M_reader_ShootEvent()
        {
            if (m_Player == null)
            {
                m_ReloadScene.RaiseEvent(m_Scene);


            }
           
        }
    }

}
