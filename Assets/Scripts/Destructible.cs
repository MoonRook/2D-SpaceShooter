using SpaseShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ������������ ������ �� �����, ������� ����� ����� ���������
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// ������ ���������� �����������
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool Isdestructible => m_Indestructible;

        /// <summary>
        /// ��������� ���������� ����������
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;


        /// <summary>
        /// ������� ���������
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;
        #endregion

        #region Unity Events
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }
        #endregion

        #region Public API
        /// <summary>
        /// ���������� damage � �������
        /// </summary>
        /// <param name = "damage"> ����, ��������� ������� </param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible || (GetComponent<SpaceShip>() != null && GetComponent<SpaceShip>().IsInvincible))
                return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
                OnDeath();
        }
        #endregion

        /// <summary>
        /// ���������������� ������� ����������� �������, ����� ��������� ���� ����
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);

            m_EventOnDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestrutibles; // ������ ������������ ��������


        // ������������� ������ � ������ ������������ ��������
        public static IReadOnlyCollection<Destructible> AllDestrutibles => m_AllDestrutibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestrutibles == null) // ���� ����������� ������������ �������� � ������ = 0
                m_AllDestrutibles = new HashSet<Destructible>(); // ������� ������ ������������ ��������

            m_AllDestrutibles.Add(this); // ������ ��������� ������ � ������ ������������ ��������
        }

        protected virtual void OnDestroy()
        {
            m_AllDestrutibles.Remove(this); // ������� ��������� ������ �� ������ ������������ ��������
        }

        public const int TeamIdNeutral = 0; // ����������� ������� = 0

        [SerializeField] private int m_TeamId; // ������ ����� �������
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private int m_ScoreValue; // ����������� ����� ���������� �� ����������� �������

        public int ScoreValue => m_ScoreValue;
    }
}


