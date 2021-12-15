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
        processor.EventCheerfulness.AddListener((state) =>
        {
            switch (state)
            {
                case Cheerfulness.Speep:
                    Debug.Log(Cheerfulness.Speep);
                    break;
                case Cheerfulness.WakeUp:
                    Debug.Log(Cheerfulness.WakeUp);
                    break;
                default:
                    break;
            }

            
        });


    }
}
