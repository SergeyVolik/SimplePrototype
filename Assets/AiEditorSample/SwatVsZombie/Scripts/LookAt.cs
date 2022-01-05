using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField]
        private Transform Target;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(Target);
        }
    }

}
