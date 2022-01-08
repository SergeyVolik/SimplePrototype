using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game.Settings
{
    public class TargetFrameRate : MonoBehaviour
    {
        public enum TargetRate
        {
            Thirty = 30,
            Sixty = 30,
            Max = 1000
        }

        [SerializeField]
        private TargetRate Rate = TargetRate.Thirty;
        // Start is called before the first frame update
        void Awake()
        {
            Application.targetFrameRate = (int)Rate;
        }

    }

}
