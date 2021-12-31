using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agents : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = AISimulationHuman.Instance.CurrentSimulationSize.ToString();
    }
}
