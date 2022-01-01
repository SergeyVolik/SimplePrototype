using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{
    public interface IMoveData
    {
        float Horizontal { get; }
        float Vertical { get; }
    }

    [DisallowMultipleComponent]
    public class PlayerInputDataComponent : MonoBehaviour, IMoveData
    {
        public float Horizontal => Input.GetAxisRaw("Horizontal");

        public float Vertical => Input.GetAxisRaw("Vertical");


    }
}
