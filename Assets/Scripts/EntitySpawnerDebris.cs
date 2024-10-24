using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefab; // ������ �������� �������� ��������� ��� ������

        [SerializeField] private CircleArea m_Area; // ���� � ������� Spawner ����� �������� �������

        [SerializeField] private int m_NumDebris; // ���������� ��������-������

        [SerializeField] private float m_RandomSpeed; // �������� �������� ����� ������

        private void Start()
        {
            for (int i = 0; i < m_NumDebris; i++) // ������� ���� �� 0 �� ������������ ���������� ��������-������
            {
                SpawnDebris(); // C���� ��������-������
            }
        }

        /// <summary>
        /// ������� �������-�����
        /// </summary>
        private void  SpawnDebris()
        {
            // �������� ��������� ������ � ������� ��������, ��������� ��� ������
            int index = Random.Range(0, m_DebrisPrefab.Length);

            GameObject debris = Instantiate(m_DebrisPrefab[index].gameObject); // �������������� ������-�����

            debris.transform.position = m_Area.GetRandomInsideZone(); // ��������� ������� ������ ���� ��������

            // �������� ��������� Destructible � ������������� �� ������� ����������� ������� ������
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>(); // �������� ��������� Rigidbody2D

            // ��������� ���� ��������� Rigidbody2D ��= 0 � ���� �������� �������� ����� ������ > 0
            if (rb != null && m_RandomSpeed > 0)
            {
                // �������� ��������� ����� * �������� �������� ����� ������
                rb.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }
        }
        
        private void OnDebrisDead()
        {
            SpawnDebris();
        }

    }
}

