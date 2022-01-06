using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SerV112.UtilityAI.Game
{
    public class InitInputAndLoadScene : MonoBehaviour
    {
        [SerializeField]
        private InputReader reader;
        void Start()
        {
            reader.EnableGameplayInput();
            SceneManager.LoadScene(1);
        }

    }

}
