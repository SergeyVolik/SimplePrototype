using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAgents : MonoBehaviour
{

    [SerializeField]
    AIAgentHuman prefab;

    void AIAgentHuman1()
    {
        
    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < 1000; i++)
            {
                Instantiate(prefab);
            }
           
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        Destroy(gameObject.GetComponent<AIAgentMYAI>());
        //    }
        //}
    }

}
