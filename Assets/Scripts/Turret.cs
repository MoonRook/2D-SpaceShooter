using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode; // Какое оружие активно
        public TurretMode Mode => m_Mode;

        [SerializeReference] private TurretProperties m_TurretProperties; // Ссылка на экземпляр класса TurretProperties
        
        private float m_RefireTimer; // Таймер повторного выстрела

        public bool CanFire => m_RefireTimer <= 0; // Булевое свойство. Если таймер повторного выстрела <= 0, то возвращаем true иначе возвращаем false

        private SpaceShip m_Ship; // Кэшированная ссылка на корабль родитель

        /// <summary>
        /// Unity Event
        /// </summary>
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }
        private void Update()
        {
           if (m_RefireTimer > 0) // Если таймер повторного выстрела > 0, то отнимаем от него частоту обновления кадра
                m_RefireTimer -= Time.deltaTime;
        }

        /// <summary>
        /// Public API
        /// </summary>
        public void Fire()
        { 
            if (m_TurretProperties == null) return;
            
            if (m_RefireTimer > 0) return;

            // Вызывает метод отнять энергию. Если энергия = 0, то выходим из метода и не создаем снаряд
            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) 
                return;

            // Вызывает метод отнять снаряды. Если количество снарядов = 0, то выходим из метода и не создаем снаряд
            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) 
                return;

            // Создаем компонент префаба снаряда ProjectilePrefab
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();

            projectile.transform.position = transform.position; // Задаем положение компоненту префаба снаряда

            projectile.transform.up = transform.up; // Задаем вращение компоненту префаба снаряда

            projectile.SetParentShooter(m_Ship); // Предотвращаем стрельбу по самому себе
            
            m_RefireTimer = m_TurretProperties.RateOfFire; // Таймер повторного выстрела = cкорострельность турели в секунду

            
            
            {
                //SFX  код для вставки звука воспроизводимого при стрельбе
            }

        }
        /// <summary>
        /// PowerUp
        /// </summary>
        /// <param name="props"></param>
        public void AssignLoadout(TurretProperties props)
        {
            // Проверяем какому оружию (основному или дополнительному) мы присваиваем свойство PowerUp
            if (m_Mode != props.Mode) return; 

            m_RefireTimer = 0; // Сбрасываем таймер на 0, при подборе PowerUp

            m_TurretProperties = props;
        }
    }
}
