using System.Collections;
using System.Collections.Generic;
using Unity.DataFlowGraph;
using UnityEngine;

namespace SerV112.UtilityAIRuntime
{

    interface IInput : ITaskPort<IInput> { }
    interface IMax : ITaskPort<IMax> { }
    interface IMin : ITaskPort<IMin> { }
}