//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEditor.GraphToolsFoundation.Overdrive;
//using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
//using UnityEngine;
//using UnityEngine.GraphToolsFoundation.Overdrive;

//namespace SerV112.UtilityAIEditor
//{
//    [Serializable]
//    public class ErrorBageModel : IErrorBadgeModel
//    {

//        public ErrorBageModel(string ErrorMessage, IGraphElementModel ParentModel, IGraphModel graph, IGraphAssetModel assetModel)
//        {
//            m_ErrorMessage = ErrorMessage;
//            m_ParentModel = ParentModel;
//            m_GraphModel = graph;
//            AssetModel = assetModel;

//        }
//        private string m_ErrorMessage;
//        public string ErrorMessage => m_ErrorMessage;

//        private IGraphElementModel m_ParentModel;
//        public IGraphElementModel ParentModel => m_ParentModel;

//        private IGraphModel m_GraphModel;
//        public IGraphModel GraphModel => m_GraphModel;

//        private SerializableGUID m_Guid;
//        public SerializableGUID Guid
//        {
//            get
//            {
//                if (!m_Guid.Valid)
//                    AssignNewGuid();
//                return m_Guid;
//            }
//            set => m_Guid = value;
//        }

//        /// <inheritdoc/>
//        public IGraphAssetModel AssetModel { get; set; }

//        /// <inheritdoc/>
//        public void AssignNewGuid()
//        {
//            m_Guid = SerializableGUID.Generate();
//        }

//        private List<Capabilities> m_Capabilities = new List<Capabilities>();
//        public IReadOnlyList<Capabilities> Capabilities => m_Capabilities;

//        public Color Color { get => Color.red; set  { } }

//        public bool HasUserColor => false;



//        public void ResetColor()
//        {
           
//        }
//    }
//}
