using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    [Serializable]
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "SimulationTime")]
    public class SimulationTimeNodeModel : MathNode
    {

        
        protected override void OnDefineNode()
        {
            base.OnDefineNode();
            AddOutputPort("output", PortType.Data, TypeHandle.Float, options: PortModelOptions.Default);
        }


        public override float Evaluate()
        {
            return 0;
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;

            if (portModel.Direction == PortDirection.Output)
                cap = PortCapacity.Multi;


            return cap;
        }
    }
}
