using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game.UI
{
    public class AndroidInputActivator : MonoBehaviour
    {
        [SerializeField]
        RectTransform AndroidInputPanel;

        void Awake()
        {
#if UNITY_ANDROID
            AndroidInputPanel.gameObject.SetActive(true);
#else
            AndroidInputPanel.gameObject.SetActive(false);
#endif
        }


    }

}