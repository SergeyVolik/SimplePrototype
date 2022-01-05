using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SerV112.UtilityAI.Game
{

    public interface IShootAIInpuData : IShootInpuData
    {

    }



    [DisallowMultipleComponent]
    public class ShootAIInputDataComponent : AbstractAIInput, IShootAIInpuData
    {

    }

}
