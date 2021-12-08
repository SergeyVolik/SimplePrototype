using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{

    public class AIToolSettingsWindow : EditorWindow
    {
        string newNamespace = "MyCompany.MyProject";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;

        private AIGraphAssetModel m_AssetModel;

        
        public static void Init(AIGraphAssetModel AssetModel)
        {

            var window = EditorWindow.GetWindow<AIToolSettingsWindow>("Settings", true);
            // Get existing open window or if none, make a new one:
            //= (AIToolSettingsWindow)EditorWindow.GetWindow(typeof(AIToolSettingsWindow));
            //window.titleContent = new GUIContent("Settings");
            window.ShowAuxWindow();
            window.InitInternal(AssetModel);

             
        }

        private void InitInternal(AIGraphAssetModel AssetModel)
        {
            m_AssetModel = AssetModel;
            newNamespace = m_AssetModel.Namespace;
        }

        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            groupEnabled = EditorGUILayout.BeginToggleGroup("Use namespace", groupEnabled);
           
            newNamespace = EditorGUILayout.TextField("Namespace", newNamespace);

            if (!groupEnabled)
            {

                if (!string.IsNullOrEmpty(m_AssetModel.Namespace))
                {
                    m_AssetModel.Namespace = string.Empty;
                }
            }
            else
            {
                if (!string.Equals(m_AssetModel.Namespace, newNamespace))
                {
                    m_AssetModel.Namespace = newNamespace;
                }
            }

            //myBool = EditorGUILayout.Toggle("Toggle", myBool);
            //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();
        }
    }
}
