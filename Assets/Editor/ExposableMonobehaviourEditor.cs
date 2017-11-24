using UnityEditor;
using UnityEngine;
using System.Collections;
using Assets.GameLogic.Utility;

namespace Assets.Editor
{
    [CustomEditor(typeof(ExposableMonobehaviour), true)]
    public class ExposableMonobehaviourEditor : UnityEditor.Editor
    {
        ExposableMonobehaviour m_Instance;
        PropertyField[] m_fields;

        public virtual void OnEnable()
        {
            m_Instance = target as ExposableMonobehaviour;
            m_fields = ExposeProperties.GetProperties(m_Instance);
        }

        public override void OnInspectorGUI()
        {
            if (m_Instance == null)
                return;
            this.DrawDefaultInspector();
            ExposeProperties.Expose(m_fields);
        }
    }
}