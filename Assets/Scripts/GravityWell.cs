using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof (CircleCollider2D))] // Добавляем ссылку на компонент CircleCollider2D
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float m_Force; // Сила притяжения
        [SerializeField] private float m_Radius; // Радиус действия силы притяжения

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return; // Проверяем collision на наличие компонента Rigidbody

            Vector2 dir = transform.position - collision.transform.position; // расстояние вектор 2 = позиция трансформы - позиция трансформы коллизии

            float dist = dir.magnitude; // Дистанция = магнитуда расстояния вектор 2

            if (dist < m_Radius)// Если дистанция меньше радиуса действия силы притяжения
            {
                // Создаем силовой вектор 2 = нормализованное направление * сила притяжения * (дистанция / радиус действия силы притяжения)
                Vector2 force = dir.normalized * m_Force * (dist / m_Radius);

                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }

        }
    #if UnityEditor
        
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
    # endif 
    }
}
