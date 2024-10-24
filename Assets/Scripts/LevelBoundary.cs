using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : SingletonBase<LevelBoundary>
    {
        [SerializeField] private float m_Radius; // Радиус ограниченвающий мир в сцене
        public float Radius => m_Radius;

        public enum Mode
        {
            Limit, // ограничение
            Teleport // перемещение в определенную точку мира
        }

        [SerializeField] private Mode m_LimitMode; // режим ограничения
        public Mode LimitMode => m_LimitMode;

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius); // Граница мира
        }
#endif

    }
}
