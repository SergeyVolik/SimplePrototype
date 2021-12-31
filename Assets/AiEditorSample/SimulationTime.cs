using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationTime : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = AISimulationHuman.Instance.m_SimulationTime.ToString();
    }
}
