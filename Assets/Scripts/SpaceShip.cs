using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent (typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [SerializeField] private Sprite m_PreviewImage;

        /// <summary>
        /// Масса для автоматической установки у rigidbody
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Сила, толкающая вперед 
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Сила вращающая 
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Максимально линейная скорость
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// Максимально вращательная скорость градусов/сек
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        private Rigidbody2D m_Rigid; // Ссылка на компонент Rigidbody

        public float MaxLinearVelocity => m_MaxLinearVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;
        public Sprite PreviewImage => m_PreviewImage;

        #region Public API
        /// <summary>
        /// Управление линейной тягой -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращательной тягой -1.0 до +1.0
        /// </summary>
        public float TorqueControl { get; set; }
        #endregion

        #region Unity Event
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
            
            UpdateEnergyRegen();

            UpdateInvincibility(); // Обновляем неуязвимость
        }
        #endregion

        /// <summary>
        /// Метод добавления сил для движения корабля
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
           
            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] m_Turrets; // Массив турелей

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy; // Мах показатель энергии
        [SerializeField] private int m_MaxAmmo; // Мах показатель снарядов

        [SerializeField] private int m_EnergyRegenPersecond; // Колличество энергии востанавливаемой в секунду
        
        private float m_PrimaryEnergy; // Текущий показатель энергии
        private int m_SecondaryAmmo; // Текущий показатель снарядов

        public void AddEnergy(int e)
        {
            // Текущий показатель энергии = математическому ограничению от 0 до Мах показателя энергии
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            // Текущий показатель снарядов = математическому ограничению от 0 до Мах показателя снарядов
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        /// <summary>
        /// Восстанвливает энергию
        /// </summary>
        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float) m_EnergyRegenPersecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        /// <summary>
        /// Отнимает энергию
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }

            return false;
        }


        /// <summary>
        /// Отнимает колличество снарядов
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Назначает определенное свойство турелям
        /// </summary>
        /// <param name="props"></param>
        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }
        /// <summary>
        /// Неуязвимость
        /// </summary>
        private bool m_IsInvincible;
        private float m_InvincibilityTimer;

        public bool IsInvincible => m_IsInvincible;

        public void EnableInvincibility(float duration)
        {
            m_IsInvincible = true;
            m_InvincibilityTimer = duration;
        }
        private void UpdateInvincibility()
        {
            if (m_IsInvincible)
            {
                m_InvincibilityTimer -= Time.fixedDeltaTime;
                if (m_InvincibilityTimer <= 0f)
                {
                    m_IsInvincible = false;
                }
            }
        }
    }
}

