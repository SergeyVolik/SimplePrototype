using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SerV112.UtilityAI.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        InputReader m_reader;
        [SerializeField]
        private GameObject PlayerDeadPanel;

        [SerializeField]
        private GameSceneSO m_Scene;
        [SerializeField]
        private LoadEventChannelSO m_ReloadScene;
        private void Awake()
        {
            m_reader.JumpEvent += M_reader_JumpEvent;
        }
        private void Update()
        {
            if (player == null)
            {
                PlayerDeadPanel.SetActive(true);

            }
        }

        private void M_reader_JumpEvent()
        {
            if (player == null)
            {
                m_ReloadScene.RaiseEvent(m_Scene);


            }
           
        }
    }

}
