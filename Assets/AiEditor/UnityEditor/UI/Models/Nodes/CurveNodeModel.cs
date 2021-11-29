using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public abstract class CurveNodeModel : NodeModel, ICurveNodeModel
    {
        protected override void OnDefineNode()
        {
            base.OnDefineNode();


            AddInputPort("Input", PortType.Data, TypeHandle.Float, options: PortModelOptions.NoEmbeddedConstant);
            AddOutputPort("Output", PortType.Data, TypeHandle.Float, options: PortModelOptions.NoEmbeddedConstant);
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;
        }

    }
}
