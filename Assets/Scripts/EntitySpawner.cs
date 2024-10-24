using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode // �������� ������ Spawner ��� ������ ��� ������������ (���������)
        { 
            Start,
            Loop
        }

        [SerializeField] private Entity[] m_EntityPrefab; // ������ ��������, ������� ����� ���������� Spawner

        [SerializeField] private CircleArea m_Area; // ���� � ������� Spawner ����� �������� �������

        [SerializeField] private SpawnMode m_SpawnMode; // ������ �� ������� Spawner

        [SerializeField] private int m_NumSpawns; // ����������� ��������, ������� ����� ���������� Spawner

        [SerializeField] private float m_RespawnTime; // ������� ���������� Spawner

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start) // ���� ������� ������ Spawner = ���������
            {
                SpawnEntities();
            }
           
            m_Timer = m_RespawnTime; // �������� ������ ���������� Spawner
        }

        private void Update()
        {
            if (m_Timer > 0) // ���� ������ > 0, �� �������� � ������� ������� ���������� �����
                m_Timer -= Time.deltaTime;

            // ���� ������� ������ Spawner = ����������� � ����� ���������� ������, �� ������� ������
            if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
            {
                SpawnEntities();

                m_Timer = m_RespawnTime;
            }
        }

        /// <summary>
        /// ������� ��������
        /// </summary>
        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++) // ���� ������ ��������� �� 0 �� ������� ����������
            {
                // �������� ��������� ������ � ������� ��������, ������� ����� ���������� Spawner
                int index = Random.Range(0, m_EntityPrefab.Length);

                GameObject e = Instantiate(m_EntityPrefab[index].gameObject); // �������������� ������

                e.transform.position = m_Area.GetRandomInsideZone(); // ������ ��������� ������� ������� ��������� ���� ������
            }
        }
    }
}

