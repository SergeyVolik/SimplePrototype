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
        private GameObject PlayerDeadPanel;
        private void Update()
        {
            if (player == null)
            {
                PlayerDeadPanel.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene(0);
                }
            }
            
        }
    }

}
