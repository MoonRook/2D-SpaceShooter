using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode // Варианты работы Spawner при старте или переодически (зациклено)
        { 
            Start,
            Loop
        }

        [SerializeField] private Entity[] m_EntityPrefab; // Массив префабов, которые может заспавнить Spawner

        [SerializeField] private CircleArea m_Area; // Зона в которой Spawner может спавнить объекты

        [SerializeField] private SpawnMode m_SpawnMode; // Ссылка на вариант Spawner

        [SerializeField] private int m_NumSpawns; // Колличество объектов, которые может заспавнить Spawner

        [SerializeField] private float m_RespawnTime; // Частота обновления Spawner

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start) // Если вариант работы Spawner = стартовый
            {
                SpawnEntities();
            }
           
            m_Timer = m_RespawnTime; // Обнуляем таймер обновления Spawner
        }

        private void Update()
        {
            if (m_Timer > 0) // Если таймер > 0, то отнимает у таймера частоту обновления кадра
                m_Timer -= Time.deltaTime;

            // Если вариант работы Spawner = зацикленный и можно заспавнить объект, то спавним объект
            if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
            {
                SpawnEntities();

                m_Timer = m_RespawnTime;
            }
        }

        /// <summary>
        /// Спавнит сущности
        /// </summary>
        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++) // Цикл спавна сущностей от 0 до нужного количества
            {
                // Выбираем случайный индекс в массиве префабов, которые может заспавнить Spawner
                int index = Random.Range(0, m_EntityPrefab.Length);

                GameObject e = Instantiate(m_EntityPrefab[index].gameObject); // Инициализирует объект

                e.transform.position = m_Area.GetRandomInsideZone(); // Задает случайную позицию объекту впределах зоны спавна
            }
        }
    }
}

