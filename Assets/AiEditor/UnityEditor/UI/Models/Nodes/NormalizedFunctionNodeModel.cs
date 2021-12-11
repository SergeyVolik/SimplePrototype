using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    [Serializable]
    public abstract class NormalizedFunctionNodeModel : MathFunction
    {

        public NormalizedFunctionNodeModel()
        {
            InputType = AIStencil.NormalizedFloat;
            OutputType = AIStencil.NormalizedFloat;
        }

    }
}
