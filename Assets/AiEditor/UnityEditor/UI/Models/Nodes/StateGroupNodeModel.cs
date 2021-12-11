﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{



    [Serializable]
    [SearcherItem(typeof(AIStencil), SearcherContext.Graph, "StateGroup")]
    public class StateGroupNodeModel : NormalizedFunctionNodeModel, IScriptName, IExtendableInputPortNode
    {

       

        [SerializeField, HideInInspector]
        string m_EnumName = "Default";

        public override string Title { get => base.Title; set => base.Title = m_EnumName + " (StateGroup)"; }
        public override string DisplayTitle => Title;
        public int VerticalInputCount => m_VerticalInputCount;

        public string Name { get => m_EnumName; set => m_EnumName = value; }

        public const string InspectorLabelNameText = "Name";

        List<string> InputPorts = new List<string>();
        public StateGroupNodeModel()
        {
            InputPorts.Add($"State{InputPorts.Count}");
            m_ParameterNames = InputPorts.ToArray();
            DeadEndNode = true;
        }

        [SerializeField, HideInInspector]
        int m_VerticalInputCount = 1;
        public void AddPort()
        {
            m_VerticalInputCount++;
            InputPorts.Add($"State{InputPorts.Count}");
            m_ParameterNames = InputPorts.ToArray();
            DefineNode();

        }
        public IEnumerable<IGraphElementModel> RemovePort()
        {
            m_VerticalInputCount--;
            InputPorts.Remove(InputPorts[InputPorts.Count-1]);
            m_ParameterNames = InputPorts.ToArray();
            var ports = this.GetInputPorts().ToList();
            IEnumerable<IGraphElementModel> edgesToRemove = null;

            if (ports.Count > 0)
            {
                edgesToRemove = ports[ports.Count - 1].GetConnectedEdges().ToList();
            }

            DefineNode();

            return edgesToRemove;

        }



        public override void OnConnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {
           
        }

        /// <inheritdoc />
        public override void OnDisconnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {

        }
        public void GenereteStateGroup(string path)
        {
            List<string> @params = new List<string>();
            this.GetConnectedNodes( PortDirection.Input, PortType.Data).OfType<StateNodeModel>().ToList().ForEach(e => {
                @params.Add(e.Name);
            });

            var asset = this.GraphModel.AssetModel as AIGraphAssetModel;
            T4GenUtils.CreateEnum(path, this.Name, new CreateEnumSettings(this.Name, @params, asset.Namespace));
        }

        public override float Evaluate()
        {
            var ports = this.GetInputPorts().ToList();

            float max = float.NegativeInfinity;
            int maxIndex = 0;
            for (int i = 0; i < ports.Count; i++)
            {
                var value = ports[i].GetValue();

               
                if (value > max)
                {
                    max = value;
                    maxIndex = i;
                }
            }

            return maxIndex;
        }
    }
}
