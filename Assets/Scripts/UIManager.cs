using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AntsGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Text m_FPS;
        [SerializeField]
        private TMPro.TMP_Text m_Units;
        // Start is called before the first frame update

        public static UIManager Intsance { get; private set; }
        void Awake()
        {
            Intsance = this;
        }

        

        public void SetUnitsCount(int units)
        {
            m_Units.text = units.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            m_FPS.text = ((int)(1 / Time.deltaTime)).ToString();
        }
    }

}
