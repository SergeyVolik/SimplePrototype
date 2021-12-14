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
        processor.Event�heerfulness.AddListener((state) =>
        {
            switch (state)
            {
                case �heerfulness.Speep:
                    Debug.Log(�heerfulness.Speep);
                    break;
                case �heerfulness.WakeUp:
                    Debug.Log(�heerfulness.WakeUp);
                    break;
                default:
                    break;
            }
        });


    }


}
