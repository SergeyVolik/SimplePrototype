using System;
using System.Text.RegularExpressions;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{

    public class FloatVariableDeclarationModel : VariableDeclarationModel
    {
        public override string DisplayTitle => Title;

        //private static readonly Regex sWhitespace = new Regex(@"\s+");
        //public static string ReplaceWhitespace(string input, string replacement)
        //{
        //    return sWhitespace.Replace(input, replacement);
        //}
        //public override string GetVariableName() {


        //    return ReplaceWhitespace(Title, " ");
        //}
    }


    
}
