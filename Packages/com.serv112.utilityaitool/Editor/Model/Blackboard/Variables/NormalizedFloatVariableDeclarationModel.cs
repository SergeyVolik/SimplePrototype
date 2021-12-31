using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;
using Object = UnityEngine.Object;

namespace SerV112.UtilityAIEditor
{

    public class NormalizedFloatVariableDeclarationModel : VariableDeclarationModel, IScriptName
    {
        public override string DisplayTitle => Title;

        public string Name { get => Title; set => Title = value; }


    }
}
