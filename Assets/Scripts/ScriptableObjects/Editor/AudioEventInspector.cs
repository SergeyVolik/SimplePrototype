using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using SerV112.UtilityAI.Game;

namespace SerV112.UtilityAI.Editor
{

    [CustomEditor(typeof(AudioSettingsSO), true)]
    public class AudioEventInspector : UnityEditor.Editor
    {
        [SerializeField]
        private AudioSource m_Preview;

        public void OnEnable()
        {
            m_Preview = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        }

        public void OnDisable()
        {
            DestroyImmediate(m_Preview.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("preview"))
            {
               
                ((AudioSettingsSO)target).Play(m_Preview);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
