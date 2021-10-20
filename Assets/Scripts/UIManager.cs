using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        float m_Time;
        // Update is called once per frame
        float average;

        List<int> fps = new List<int>();
        void Update()
        {
            m_Time += Time.deltaTime;

            fps.Add(((int)(1 / Time.deltaTime)));
            if (m_Time > 0.5f)
            {
                var sum = fps.Sum();
                m_FPS.text = ((int)(sum / fps.Count)).ToString();
                m_Time = 0;
                fps.Clear();
            }

        }
    }

}
