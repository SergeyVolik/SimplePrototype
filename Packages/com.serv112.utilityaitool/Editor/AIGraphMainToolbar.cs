using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.UIElements;
using UnityEngine;


namespace SerV112.UtilityAIEditor
{
    public class AIGraphMainToolbar : MainToolbar
    {

        public AIGraphMainToolbar(CommandDispatcher commandDispatcher, GraphView graphView)
            : base(commandDispatcher, graphView) 
        {
            m_SaveAllButton.tooltip = "Save All (custom)";
            m_SaveAllButton.ChangeClickEvent(() => {
                AssetDatabase.SaveAssets();
                //OnBuildAllButton();
            });

            m_BuildAllButton.tooltip = "Build (custom)";
            m_BuildAllButton.ChangeClickEvent(OnBuildAllButton);

        }

        void OnBuildAllButton()
        {
            try
            {
                m_CommandDispatcher.Dispatch(new BuildAIEditorCommand());
                
            }
            catch (Exception e) // so the button doesn't get stuck
            {
                Debug.LogException(e);
            }
        }

        protected override void BuildOptionMenu(GenericMenu menu)
        {
            base.BuildOptionMenu(menu);

            var preferences = m_CommandDispatcher.State.Preferences;

            GUIContent CreateTextContent(string content)
            {
                // TODO: Replace by EditorGUIUtility.TrTextContent when it's made 'public'.
                return new GUIContent(content);
            }

            void MenuItem(string title, bool value, GenericMenu.MenuFunction onToggle)
                => menu.AddItem(CreateTextContent(title), value, onToggle);

            void MenuToggle(string title, BoolPref k, System.Action callback = null)
            {
                if (preferences != null)
                    MenuItem(title, preferences.GetBool(k), () =>
                    {
                        preferences.ToggleBool(k);
                        callback?.Invoke();
                    });
            }

            menu.AddSeparator("");
            MenuToggle("AutoProcess", BoolPref.AutoProcess);
            MenuToggle("AutoAlignDraggedEdges", BoolPref.AutoAlignDraggedEdges);
            MenuToggle("WarnOnUIFullRebuild", BoolPref.WarnOnUIFullRebuild);
            MenuToggle("LogUIBuildTime", BoolPref.LogUIBuildTime);
            MenuToggle("DependenciesLogging", BoolPref.DependenciesLogging);
            MenuToggle("ErrorOnRecursiveDispatch", BoolPref.ErrorOnRecursiveDispatch); 
            MenuToggle("LogAllDispatchedCommands", BoolPref.LogAllDispatchedCommands);
            MenuToggle("ShowUnusedNodes", BoolPref.ShowUnusedNodes);
            MenuToggle("SearcherInRegularWindow", BoolPref.SearcherInRegularWindow);
            MenuToggle("LogUIUpdate", BoolPref.LogUIUpdate);
            MenuToggle("LogUIUpdate", BoolPref.AutoItemizeVariables);
            MenuToggle("LogUIUpdate", BoolPref.AutoItemizeConstants);
            //MenuToggle("TestBoolPref1", AIBoolPref.TestBoolPref1);
            //MenuToggle("TestBoolPref2", AIBoolPref.TestBoolPref2);
            menu.AddSeparator("");
            menu.AddItem(
                    CreateTextContent("Open settings"),
                    false, 
                    () => {
                        AIToolSettingsWindow.Init(m_GraphView.GraphModel.AssetModel as AIGraphAssetModel, m_GraphView); 
                });
        }
    }
}
