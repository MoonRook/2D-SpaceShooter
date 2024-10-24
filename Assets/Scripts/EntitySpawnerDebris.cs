using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefab; // Массив префабов объектов доступных для спавна

        [SerializeField] private CircleArea m_Area; // Зона в которой Spawner может спавнить объекты

        [SerializeField] private int m_NumDebris; // Количество объектов-мусора

        [SerializeField] private float m_RandomSpeed; // Скорость объектов после спавна

        private void Start()
        {
            for (int i = 0; i < m_NumDebris; i++) // Создает цикл от 0 до необходимого количества объектов-мусора
            {
                SpawnDebris(); // Cпавн объектов-мусора
            }
        }

        /// <summary>
        /// Спавнит объекты-мусор
        /// </summary>
        private void  SpawnDebris()
        {
            // Выбирает случайный индекс в массиве префабов, доступных для спавна
            int index = Random.Range(0, m_DebrisPrefab.Length);

            GameObject debris = Instantiate(m_DebrisPrefab[index].gameObject); // Инициализирует объект-мусор

            debris.transform.position = m_Area.GetRandomInsideZone(); // Назначает позицию внутри зоны спавнера

            // Получает компонент Destructible и подписывается на событие уничтожения объекта спавна
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>(); // Получает компонент Rigidbody2D

            // Проверяет если компонент Rigidbody2D не= 0 и если скорость объектов после спавна > 0
            if (rb != null && m_RandomSpeed > 0)
            {
                // Получает случайную точку * скорость объектов после спавна
                rb.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }
        }
        
        private void OnDebrisDead()
        {
            SpawnDebris();
        }

    }
}

