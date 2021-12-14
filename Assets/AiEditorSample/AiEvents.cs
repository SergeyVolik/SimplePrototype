using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEvents : MonoBehaviour
{
    AIProcessor processor;
    // Start is called before the first frame update
    void Awake()
    {
        processor = GetComponent<AIProcessor>();
        processor.EventÑheerfulness.AddListener((state) =>
        {
            switch (state)
            {
                case Ñheerfulness.Speep:
                    Debug.Log(Ñheerfulness.Speep);
                    break;
                case Ñheerfulness.WakeUp:
                    Debug.Log(Ñheerfulness.WakeUp);
                    break;
                default:
                    break;
            }
        });


    }


}
