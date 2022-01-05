using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{
    public class AIGraphTrace : IGraphTrace
    {
        public IReadOnlyList<IFrameData> AllFrames => m_AllFrames;

        private List<IFrameData> m_AllFrames = new List<IFrameData>();

        public AIGraphTrace()
        {
            var frame = new AIGraphFrameData(0);
            m_AllFrames.Add(frame);

        }

        public void AddFrame(AIGraphFrameData frame)
        {
            m_AllFrames.Add(frame);
        }
    }

    public class AIGraphFrameData : IFrameData
    {
        public int Frame => m_Frame;

        private int m_Frame;

        public AIGraphFrameData(int frame)
        {
            m_Frame = frame;
        }

        public List<TracingStep> Targets = new List<TracingStep>();


        public IEnumerable<TracingStep> GetDebuggingSteps(Stencil stencil)
        {
            return Targets;
        }
    }

    public class AIGraphDebugger : IDebugger
    {

        Dictionary<int, AIGraphTrace> Targets = new Dictionary<int, AIGraphTrace>();
        Dictionary<NodeModel, AIGraphTrace> NodesTrace = new Dictionary<NodeModel, AIGraphTrace>();
        Dictionary<int, string> TargetTitles = new Dictionary<int, string>();
        List<int> targetIndices = new List<int>();
        public IEnumerable<int> GetDebuggingTargets(IGraphModel graphModel)
        {
            return targetIndices;
        }

        public IGraphTrace GetGraphTrace(IGraphModel assetModelGraphModel, int currentTracingTarget)
        {
            Debug.Log($"GetGraphTrace {currentTracingTarget}");
            if (Targets.TryGetValue(currentTracingTarget, out var trace))
            {
                return trace;
            }

            return trace;
        }

        public string GetTargetLabel(IGraphModel graphModel, int target)
        {
            if (TargetTitles.TryGetValue(target, out var label))
            {
                return label;
            }

            return label;
        }

        public bool GetTracingSteps(IGraphModel currentGraphModel, int frame, int tracingTarget, out List<TracingStep> stepList)
        {
            //stepList = m_AiGraphTrace.AllFrames.ToList()[frame].GetDebuggingSteps();
            stepList = null;

            //if (Targets.TryGetValue(tracingTarget, out var trace))
            //{
            //    stepList = trace.AllFrames.FirstOrDefault(e => e.Frame == frame).GetDebuggingSteps();
            //}
            return false;
        }

        public void OnToggleTracing(IGraphModel currentGraphModel, bool enabled)
        {
            Debug.Log($"OnToggleTracing {enabled}");
        }

        public void Start(IGraphModel graphModel, bool tracingEnabled)
        {
            Debug.Log($"AIGraphDebugger:Start {tracingEnabled}");
            Targets.Clear();
            TargetTitles.Clear();
            targetIndices.Clear();
            NodesTrace.Clear();
            if (tracingEnabled)
            {
                int targetIndex = 0;
                graphModel.VariableDeclarations.OfType<NormalizedFloatVariableDeclarationModel>().ToList().ForEach(e =>
                {
                    Targets.Add(targetIndex, new AIGraphTrace());

                    TargetTitles.Add(targetIndex, e.Title);
                    targetIndices.Add(targetIndex);
                    targetIndex++;

                });

                graphModel.NodeModels.OfType<StateGroupNodeModel>().ToList().ForEach(e =>
                {
                    var trace = new AIGraphTrace();
                    NodesTrace.Add(e, trace);
                    Targets.Add(targetIndex, trace);
                    TargetTitles.Add(targetIndex, e.Title);
                    targetIndices.Add(targetIndex);
                    targetIndex++;

                });

                graphModel.NodeModels.OfType<StateNodeModel>().ToList().ForEach(e =>
                {
                    var trace = new AIGraphTrace();
                    NodesTrace.Add(e, trace);
                    Targets.Add(targetIndex, trace);
                    TargetTitles.Add(targetIndex, e.Title);
                    targetIndices.Add(targetIndex);
                    targetIndex++;

                });
            }

        }

        public void Stop()
        {
            Debug.Log("AIGraphDebugger:Stop");
            Targets.Clear();
            TargetTitles.Clear();
            targetIndices.Clear();
            NodesTrace.Clear();
        }
    }

    public class MyCustomPluginHandler : IPluginHandler
    {
        public void OptionsMenu(GenericMenu menu)
        {
           
        }

        public void Register(GraphViewEditorWindow window)
        {
            //Debug.Log("MyCustomPluginHandler has been registered");
        }

        public void Unregister()
        {
            //Debug.Log("MyCustomPluginHandler has been unregistered");
        }
    }
}
