using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class TrailEffect : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_TargetShip;
        [SerializeField] private float m_TrailDuration;
        [SerializeField] private float m_TrailWidth;

        private TrailRenderer m_TrailRenderer;

        private void Start()
        {
            m_TrailRenderer = gameObject.AddComponent<TrailRenderer>();
            m_TrailRenderer.time = m_TrailDuration;
            m_TrailRenderer.startWidth = m_TrailWidth;
            m_TrailRenderer.endWidth = 0f;
        }

        private void Update()
        {
            if (m_TargetShip == null) return;

            // Обновляем положение хвоста в зависимости от движения корабля
            transform.position = m_TargetShip.transform.position;
            transform.rotation = m_TargetShip.transform.rotation;

            // Проверяем, есть ли движение корабля
            if (Mathf.Abs(m_TargetShip.ThrustControl) > 0.1f)
            {
                // Включаем отображение хвоста
                m_TrailRenderer.emitting = true;
            }
            else
            {
                // Выключаем отображение хвоста
                m_TrailRenderer.emitting = false;
            }
        }
    }
}
