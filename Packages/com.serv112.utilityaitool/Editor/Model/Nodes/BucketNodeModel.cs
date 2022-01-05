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
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "Bucket")]
    public class BucketNodeModel : ExtendableInputPortNode
    {

        public BucketNodeModel()
        {
            InputType = AIGraphCustomTypes.AIGroup;
            OutputType = AIGraphCustomTypes.AIBucket;
            
            
            //m_ParameterNames = InputPorts.ToArray();

        }      

        public override float Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
