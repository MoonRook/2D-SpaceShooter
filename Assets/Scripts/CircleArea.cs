using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// Зона в которой Spawner может спавнить объекты
    /// </summary>
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float m_Radius; // Радиус окружности
        public float Radius => m_Radius;

        public Vector2 GetRandomInsideZone() // Получает точку в центре окружности
        {
            return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_Radius;
        }

        #if UNITY_EDITOR 
        
        private static Color GizmoColor = new Color(0, 1, 0, 0.1f); // Задает цвет и прозрачность окружности

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor; // Присваивает цвет окружности
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius); // Рисует окружность
        }
        
        #endif
    }
}
