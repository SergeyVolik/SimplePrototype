using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Text;

public class CodeViewWindow : EditorWindow
{
    [MenuItem("Window/UI Toolkit/CodeViewWindow")]
    public static void ShowExample()
    {
        CodeViewWindow wnd = CreateWindow<CodeViewWindow>();
        wnd.titleContent = new GUIContent("CodeViewWindow");
    }

    public static void ShowCode(string code, string title)
    {
        CodeViewWindow wnd = CreateWindow<CodeViewWindow>();
        wnd.titleContent = new GUIContent(title);
        wnd.m_CodeLines.SetValueWithoutNotify(code);
        StringBuilder builder = new StringBuilder("");
        for (var i = 1; i < CountLines(code); i++)
        {
            builder.AppendLine(i.ToString() + ".");
        }

        wnd.m_NumberOfCode.text = builder.ToString();
    }

    Label m_NumberOfCode;
    TextField m_CodeLines;

    const string Code = @"// Action to perform when button is pressed.
// Toggles the text on all buttons in 'container'.
Action action = () =>
{
    container.Query<Button>().ForEach((button) =>
    {
        button.text = button.text.EndsWith('Button') ? 'Button(Clicked)' : 'Button';
    });
};

// Get a reference to the Button from UXML and assign it its action.
var uxmlButton = container.Q<Button>('the-uxml-button');
uxmlButton.RegisterCallback<MouseUpEvent>((evt) => action());

// Create a new Button with an action and give it a style class.
var csharpButton = new Button(action) { text = 'C# Button' };
csharpButton.AddToClassList('some-styled-button');
container.Add(csharpButton);";

    private static int CountLines(string str)
    {
       return  str.Split('\n').Length;
    }
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        //VisualElement label = new Label("Hello World! From C#");
        //root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/AiEditor/UnityEditor/CodeViewWindow/CodeViewWindow.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/AiEditor/UnityEditor/CodeViewWindow/CodeViewWindow.uss");

        root.styleSheets.Add(styleSheet);

        m_NumberOfCode = root.Q<Label>("LinesOfCode");
        m_CodeLines = root.Q<TextField>("Code");

       
        m_CodeLines.isReadOnly = true;


        //root.Add(labelWithStyle);
    }
}