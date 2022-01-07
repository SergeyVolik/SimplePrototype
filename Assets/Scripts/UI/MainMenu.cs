using SerV112.UtilityAI.Game.Channels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Listening to")]
        [SerializeField]
        private VoidEventChannelSO m_Quit;

        [SerializeField]
        private GamePlaySceneSO m_Scene;
        [SerializeField]
        [Header("Broadcasting on")]
        private LoadEventChannelSO m_LoadGameplayScene;
        // Start is called before the first frame update
        private void OnEnable()
        {
            m_Quit.OnEventRaised += Quit;
        }
        private void OnDisable()
        {
            m_Quit.OnEventRaised -= Quit;
        }


        public void StartGame()
        {
            m_LoadGameplayScene.RaiseEvent(m_Scene);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }

}
