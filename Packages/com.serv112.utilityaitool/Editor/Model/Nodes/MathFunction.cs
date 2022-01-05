using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    [Serializable]
    public abstract class MathFunction : MathNode
    {

        [SerializeField]
        protected string[] m_ParameterNames = new string[0];

        protected TypeHandle InputType { get; set; } = TypeHandle.Float;
        protected TypeHandle OutputType { get; set; } = TypeHandle.Float;

        protected bool DeadEndNode { get; set; } = false;
        protected bool NoEmbeddedConstant { get; set; } = true;
        public float GetParameterValue(int index)
        {
            var port = this.GetInputPorts().Skip(index).FirstOrDefault();

            if (port == null)
            {
                Debug.LogError("Access to unavailable port " + index);
                return 0;
            }

            return GetValue(port);
        }

        public IPortModel DataOut0 { get; private set; }

        protected override void OnDefineNode()
        {

            foreach (var name in m_ParameterNames)
            {
                //this.AddDataInputPort<float>(name);
                PortModelOptions options = PortModelOptions.Default;
                if (NoEmbeddedConstant)
                    options = PortModelOptions.NoEmbeddedConstant;
                this.AddDataInputPort(name, InputType, options: options);
            }
            

            if(!DeadEndNode)
                DataOut0 = this.AddDataOutputPort("output", OutputType);//this.AddDataOutputPort<float>("out");


        }
    }
}
