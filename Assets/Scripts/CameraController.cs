using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera; // то, что следит за объектом
        
        [SerializeField] private Transform m_Target; // объект слежения, цель

        [SerializeField] private float m_InterpolationLianer; // скорость интерполяции

        [SerializeField] private float m_InterpolationAngular; // скорость угловой интерполяции

        [SerializeField] private float m_CameraZOffset; // смещение по оси Z

        [SerializeField] private float m_ForwardOffset; // смещение по направлению движения

        private void FixedUpdate()
        {
            if (m_Target == null || m_Camera == null) return; // проверка на наличие объекта слежения и камеры

            Vector2 camPos = m_Camera.transform.position; // вектор позиции камеры = позиция камеры

            // целевая позиция = позиция цели
            // + направление движения цели * смещение по направлению движения 
            Vector2 targePos = m_Target.position + m_Target.transform.up * m_ForwardOffset; 

            // новая позиция камеры = интерполяция вектора 2 (между текущей позиции камеры, целевой позиции камеры,
            // скоростью интерполяции * на время обновления кадра)
            Vector2 newcamPos = Vector2.Lerp(camPos, targePos, m_InterpolationLianer * Time.deltaTime);

            // позиция камеры = новый вектор 3 (новая позиция камеры по х и y, смещение по оси Z)
            m_Camera.transform.position = new Vector3(newcamPos.x, newcamPos.y, m_CameraZOffset); 

            // проверка на скорость поворота
            if (m_InterpolationAngular > 0)
            {
                // поворот камеры = интерполяция кватерниона (текущее положении поворота камеры, текущий поворот целевого объекта,
                // скорость интерполяции * на время обновления кадра)
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation,
                  m_Target.rotation, m_InterpolationAngular * Time.deltaTime);
            }
        }
        public void SetTarget(Transform newTarget)
        {
            m_Target = newTarget; // задаем новую трансформу для слежения
        }
    }
}
