using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Text;

namespace SerV112.UtilityAIEditor
{


    public abstract class CloseAfterdReloadScriptsWindow : EditorWindow
    {
        protected static bool NeedClose = true;
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            NeedClose = true;
        }

        protected virtual void CreateGUI()
        {
            if (NeedClose)
            {
                Close();
            }
        }
        protected static void Open<T>(bool mult, params object[] UserData) where T : CloseAfterdReloadScriptsWindow
        {
            NeedClose = false;
            T wnd;
            if (mult)
            {
                wnd = CreateWindow<T>();
            }
            else
            {
                wnd = GetWindow<T>();
            }
            wnd.OpenInternal(UserData);


        }


        protected abstract void OpenInternal(params object[] UserData);

    }

    public class CodeViewWindow : CloseAfterdReloadScriptsWindow
    {


        public static void OpenCodeViewWindow(string code, string title)
        {
            Open<CodeViewWindow>(true, code, title);
        }

        protected override void OpenInternal(params object[] UserData)
        {
            string code = UserData[0].ToString();
            string title = UserData[1].ToString();

            titleContent = new GUIContent(title);

            m_CodeLines.SetValueWithoutNotify(code);
            StringBuilder builder = new StringBuilder("");
            for (var i = 1; i < CountLines(code); i++)
            {
                builder.AppendLine(i.ToString() + ".");
            }

            m_NumberOfCode.text = builder.ToString();
        }


        Label m_NumberOfCode;
        TextField m_CodeLines;

        private static int CountLines(string str)
        {
            return str.Split('\n').Length;
        }


        protected override void CreateGUI()
        {
            base.CreateGUI();
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // VisualElements objects can contain other VisualElement following a tree hierarchy.
            //VisualElement label = new Label("Hello World! From C#");
            //root.Add(label);

            // Import UXML

            var pathUxml = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeViewWindow/CodeViewWindow.uxml");
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(pathUxml);
            VisualElement labelFromUXML = visualTree.Instantiate();
            root.Add(labelFromUXML);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var pathStyle = string.Join("/", DirectoryUtils.DefaultPath, "Editor/CodeViewWindow/CodeViewWindow.uss");
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(pathStyle);

            root.styleSheets.Add(styleSheet);

            m_NumberOfCode = root.Q<Label>("LinesOfCode");
            m_CodeLines = root.Q<TextField>("Code");


            m_CodeLines.isReadOnly = true;


            //root.Add(labelWithStyle);
        }
    }
}