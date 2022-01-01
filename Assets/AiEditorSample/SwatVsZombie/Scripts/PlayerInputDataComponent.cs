using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{
    public interface IMoveData
    {
        float Horizontal { get; }
        float Vertical { get; }
    }

    public interface FireEvent
    {
        UnityEvent Fire { get; }
    }

    public interface JumpEvent
    {
        UnityEvent Jump { get; }
    }

    public interface RunEvents
    {
        UnityEvent RunStart { get; }
        UnityEvent RunStop { get; }
    }

    public interface AimEvents
    {
        UnityEvent AimStart { get; }
        UnityEvent AimStop { get; }
    }

    [DisallowMultipleComponent]
    public class PlayerInputDataComponent : MonoBehaviour, IMoveData
    {
        public float Horizontal => Input.GetAxisRaw("Horizontal");

        public float Vertical => Input.GetAxisRaw("Vertical");

    }
}
