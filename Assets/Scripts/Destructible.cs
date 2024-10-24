using SpaseShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объект на сцене, который может иметь хитпоинты
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Объект игнорирует повреждения
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool Isdestructible => m_Indestructible;

        /// <summary>
        /// Стартовое количество хитпоинтов
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;


        /// <summary>
        /// Текущие хитпоинты
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
        /// Применение damage к объекту
        /// </summary>
        /// <param name = "damage"> Урон, наносимый объекту </param>
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
        /// Переопределяемое событие уничтожения объекта, когда хитпоинты ниже нуля
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);

            m_EventOnDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestrutibles; // Список уничтожаемых объектов


        // Предоставляет доступ к списку уничтожаемых объектов
        public static IReadOnlyCollection<Destructible> AllDestrutibles => m_AllDestrutibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestrutibles == null) // Если колличество уничтожаемых объектов в списке = 0
                m_AllDestrutibles = new HashSet<Destructible>(); // Создает список уничтожаемых объектов

            m_AllDestrutibles.Add(this); // Вносит экземпляр класса в список уничтожаемых объектов
        }

        protected virtual void OnDestroy()
        {
            m_AllDestrutibles.Remove(this); // Удаляет экземпляр класса из списка уничтожаемых объектов
        }

        public const int TeamIdNeutral = 0; // Нейтральная команда = 0

        [SerializeField] private int m_TeamId; // Задает число команде
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private int m_ScoreValue; // Колличество очков получаемое за уничтожение объекта

        public int ScoreValue => m_ScoreValue;
    }
}


