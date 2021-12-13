using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public static class PortModelExtension
    {
        public static float GetValue(this IPortModel self)
        {
            if (self == null)
                return 0;
            var node = self.GetConnectedEdges().FirstOrDefault()?.FromPort.NodeModel;

            switch (node)
            {
                case MathNode mathNode:
                    return mathNode.Evaluate();
                case IVariableNodeModel varNode:
                    return (float)varNode.VariableDeclarationModel.InitializationModel.ObjectValue;
                case IConstantNodeModel constNode:
                    return (float)constNode.ObjectValue;
                case IEdgePortalExitModel portalModel:
                    var oppositePortal = portalModel.GraphModel.FindReferencesInGraph<IEdgePortalEntryModel>(portalModel.DeclarationModel).FirstOrDefault();
                    if (oppositePortal != null)
                    {
                        return oppositePortal.InputPort.GetValue();
                    }
                    return 0;
                default:
                    return (float)self.EmbeddedValue.ObjectValue;
            }
        }
    }
}
