using System;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace SerV112.UtilityAIEditor
{
    public class AIGraphModel : GraphModel
    {
        protected override bool IsCompatiblePort(IPortModel startPortModel, IPortModel compatiblePortModel)
        {
            return base.IsCompatiblePort(startPortModel, compatiblePortModel) &&
                startPortModel.DataTypeHandle == compatiblePortModel.DataTypeHandle;
        }
    }
}
