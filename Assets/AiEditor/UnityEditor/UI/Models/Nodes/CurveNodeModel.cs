using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public abstract class CurveNodeModel : NodeModel, ICurveNodeModel
    {
        [SerializeField, HideInInspector]
        private float m_Min;
        [SerializeField, HideInInspector]
        private float m_Max;
        public float MinNormalizationValue { get => m_Min; set => m_Min = value; }
        public float MaxNormalizationValue { get => m_Max; set => m_Max = value; }

        protected override void OnDefineNode()
        {
            base.OnDefineNode();


            AddInputPort("Input", PortType.Data, TypeHandle.Float, options: PortModelOptions.Default, portId: "MyPort");
            AddOutputPort("Output", PortType.Data, TypeHandle.Float, options: PortModelOptions.Default);
        }

        public override PortCapacity GetPortCapacity(IPortModel portModel)
        {
            PortCapacity cap = PortCapacity.Single;
            return cap;
        }

    }
}
