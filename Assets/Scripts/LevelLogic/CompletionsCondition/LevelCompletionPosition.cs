using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelCompletionPosition : LevelCondition
    {
        [SerializeField] private float m_Radius; // Задает радиус

        public override bool IsCompleted
        {
            get
            {
                // Проверяет если сейчас активный корабль
                if (Player.Instance.ActiveShip == null) return false;

                // Если дистанция от корабля игрока до текущей позиии <= заданному радиусу
                if (Vector3.Distance(Player.Instance.ActiveShip.transform.position, transform.position) <= m_Radius)
                {
                    return true;
                }
                
                return false;
            }
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

