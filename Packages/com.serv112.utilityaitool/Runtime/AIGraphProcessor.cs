using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SerV112.UtilityAIEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIRuntime
{


    public abstract class AIGraphProcessor : MonoBehaviour
    {
        [SerializeField]
        private AIGraphAssetModel m_Asset;

        private AIGraphAssetModel m_Clone;

        private Dictionary<string, IConstant> m_Variables;
        private List<StateGroupNodeModel> m_EntryNodes;

        protected virtual void Awake()
        {
            m_Clone = Instantiate(m_Asset);
            m_Variables = new Dictionary<string, IConstant>();
            var list = m_Clone.GraphModel.VariableDeclarations.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                m_Variables.Add(list[i].GetVariableName(), list[i].InitializationModel);
            }

            m_EntryNodes = m_Clone.GraphModel.NodeModels.OfType<StateGroupNodeModel>().ToList();
        }
        public int Execute(int entryNodeIndex)
        {
            if (m_EntryNodes.Count <= entryNodeIndex)
            {
                Debug.LogError("The executing node does not exist");
                return -1;
            }

            return (int)m_EntryNodes[entryNodeIndex].Evaluate();
        }

        public void SetProperty(string name, float value) {


            if (m_Variables.TryGetValue(name, out var result))
            {
                result.ObjectValue = value;
            }
            else {
                Debug.LogError("The property does not exist");
            }
            
        }

        public float GetProperty(string name)
        {


            if (m_Variables.TryGetValue(name, out var result))
            {
                return (float)result.ObjectValue;
            }
            else
            {
                Debug.LogError("The property does not exist");
            }
            return 0;

        }

    }


}
