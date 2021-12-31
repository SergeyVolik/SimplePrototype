using System;
using System.Collections.Generic;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;
using UnityEngine;

namespace SerV112.UtilityAIEditor
{


    [Serializable]
    public class ToolSettingsWindowStateComponent : AssetStateComponent<ToolSettingsWindowStateComponent.StateUpdater>
    {
        /// <summary>
        /// The updater for the <see cref="ToolSettingsWindowStateComponent"/>.
        /// </summary>
        public class StateUpdater : BaseUpdater<ToolSettingsWindowStateComponent>
        {
            public void MarkChangedNamespace(string @namespace)
            {
                m_State.m_Namespace = @namespace;
                m_State.SetUpdateType(UpdateType.Complete);
            }

            public void MarkChangeBuildMode(BuildMode value)
            {
                m_State.m_BuildMode = value;
                m_State.SetUpdateType(UpdateType.Complete);
            }

            public void MarkChangeGPUPrecision(GPUPrecision value)
            {
                m_State.m_GPUPrecision = value;
                m_State.SetUpdateType(UpdateType.Complete);
            }

            public void MarkChangeDebug(bool value)
            {
                m_State.m_Debug = value;
                m_State.SetUpdateType(UpdateType.Complete);
            }
        }

       

        [SerializeField]
        private string m_Namespace;
        [SerializeField]
        private GPUPrecision m_GPUPrecision = GPUPrecision.@float;
        [SerializeField]
        private BuildMode m_BuildMode;
        [SerializeField]
        private bool m_Debug;
        public bool Debug => m_Debug;
        public string Namespace => m_Namespace;
        public BuildMode BuildType => m_BuildMode;
        public GPUPrecision GPUPrecision => m_GPUPrecision;
        public ToolSettingsWindowStateComponent()
        {

        }


        protected override void Dispose(bool disposing)
        {
        }
    }
}
