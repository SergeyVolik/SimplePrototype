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
    public abstract class CurveNodeModel : NormalizedFunctionNodeModel, ICurveNodeModel
    {

        public CurveNodeModel()
        {
            m_ParameterNames = new string[] { "Input" };
        }

        public IPortModel InputPort => this.InputsById["Input"];


    }
}
