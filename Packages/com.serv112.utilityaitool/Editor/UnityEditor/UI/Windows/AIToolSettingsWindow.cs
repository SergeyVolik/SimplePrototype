using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;
using UnityEngine.UIElements;
using static SerV112.UtilityAIEditor.ToolSettingsWindowStateComponent;

namespace SerV112.UtilityAIEditor
{

    public class AIToolSettingsWindow : CloseAfterdReloadScriptsWindow
    {
        class StateObserver : StateObserver<AIState>
        {

            AIToolSettingsWindow m_Window;
            public StateObserver(AIToolSettingsWindow window)
                : base(nameof(AIState.ToolSettingsState))
            {
                m_Window = window;
            }

            protected override void Observe(AIState state)
            {
                using (var observation = this.ObserveState(state.ToolSettingsState))
                {
                    if (observation.UpdateType != UpdateType.None)
                    {
                        Debug.Log(state.ToolSettingsState.Namespace);
                        m_Window.m_Namespace.SetValueWithoutNotify(state.ToolSettingsState.Namespace);
                        Debug.Log(state.ToolSettingsState.BuildType);
                        m_Window.m_BuildMode.SetValueWithoutNotify(state.ToolSettingsState.BuildType);
                    }
                }
            }

        }



        private AIGraphAssetModel m_AssetModel;
        private GraphView m_GraphView;
        private StateObserver observer;

        public static void Init(AIGraphAssetModel AssetModel, UnityEditor.GraphToolsFoundation.Overdrive.GraphView GraphView)
        {
            Open<AIToolSettingsWindow>(false, AssetModel, GraphView);

        }

        public bool Check = true;

        private TextField m_Namespace;
        //public TextField Namespace => m_Namespace;
        //public EnumField BuildType => m_BuildMode;
        private Label m_SettingsLabel;
        private EnumField m_BuildMode;

        private void OnEnable()
        {
            
            minSize = new Vector2(200, 200);

            var pathUxml = string.Join("/", DirectoryUtils.DefaultPath, "Editor/UnityEditor/SettingsWindow.uxml");
            VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(pathUxml);
            uiAsset.CloneTree(rootVisualElement);

            m_SettingsLabel = rootVisualElement.SafeQ<Label>("SettingsName");
            m_Namespace = rootVisualElement.SafeQ<TextField>("Namespace");
            m_BuildMode = rootVisualElement.SafeQ<EnumField>("build-mode");


            m_BuildMode.Init(BuildMode.MonoBehaviour);

            m_Namespace.RegisterCallback<FocusInEvent>(OnFocusInTextField);
            m_Namespace.RegisterCallback<FocusOutEvent>(OnFocusOutTextField);
            m_BuildMode.RegisterCallback<ChangeEvent<Enum>>((evt) =>
            {

                m_GraphView.CommandDispatcher.Dispatch(new SetBuildModeCommand((BuildMode)evt.newValue, m_AssetModel));
                
            });

        }

        string m_OnFocusStartText;
        private void OnFocusInTextField(FocusInEvent evt)
        {

            var textField = evt.target as TextField;
            m_OnFocusStartText = textField.text;


        }

        private void OnFocusOutTextField(FocusOutEvent evt)
        {
            var textField = evt.target as TextField;
            if (textField.text != m_OnFocusStartText)
            {
                Debug.Log(textField.text);
                m_GraphView.CommandDispatcher.Dispatch(new SetNamespaceNameCommand(textField.text, m_AssetModel));
            }
        }

        private void OnDestroy()
        {
            m_GraphView?.CommandDispatcher?.UnregisterObserver(observer);
        }

        private void OnDisable()
        {
            m_GraphView?.CommandDispatcher?.UnregisterObserver(observer);
        }


        protected override void OpenInternal(params object[] UserData)
        {
            m_AssetModel = UserData[0] as AIGraphAssetModel;
            m_GraphView = UserData[1] as GraphView;

            titleContent = new GUIContent("Settings");
            m_GraphView.CommandDispatcher.UnregisterObserver(observer);
            observer = new StateObserver(this);
            m_Namespace.SetValueWithoutNotify(m_AssetModel.Namespace);
           
            m_GraphView.CommandDispatcher.RegisterObserver(observer);
            m_SettingsLabel.text = m_AssetModel.Name + " Settings";


            m_BuildMode.Init(BuildMode.MonoBehaviour);
            m_BuildMode.SetValueWithoutNotify(m_AssetModel.BuildMode);
            
        }
    }
}
