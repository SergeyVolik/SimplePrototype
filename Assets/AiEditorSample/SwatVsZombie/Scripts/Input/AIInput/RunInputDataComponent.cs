using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerV112.UtilityAI.Game
{

    public interface IRumAIInpuData : IRunInputData
    {
    }
    [DisallowMultipleComponent]
    public class RunInputDataComponent : AbstractAIInputStartEnd, IRumAIInpuData
    {

    }

}
