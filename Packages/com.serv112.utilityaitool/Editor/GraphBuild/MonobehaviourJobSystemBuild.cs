using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{


    public class MonobehaviourJobSystemBuild : BaseBuild
    {

        public MonobehaviourJobSystemBuild(AIGraphModel GraphModel) : base(GraphModel)
        {

        }

        void MonoBehaviourBuild()
        {         

            JobSystemCodeGen generator = new JobSystemCodeGen(m_AssetModel, m_AIGraphModel.Stencil as AIStencil, "TestJobSystem", pathWithScripts);
            generator.GenerateAndSave();

            
          

            State = AIGraphBuidState.AfterReimport;
            AssetDatabase.Refresh();

        }

        public override void Build()
        {
            MonoBehaviourBuild();
        }
    }
}
