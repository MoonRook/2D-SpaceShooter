using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public enum TurretMode
    {
        Primary, // Первичная турель
        Secondary // Вторичная турель
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject // Наследуем класс от ScriptableObject 
    {
        [SerializeField] private TurretMode m_Mode; // Турель
        public TurretMode Mode => m_Mode;

        [SerializeField] private Projectile m_ProjectilePrefab; // Ссылка на префаб снаряда
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        [SerializeField] private float m_RateOfFire; // Скорострельность турели в секунду
        public float RateOfFire => m_RateOfFire;

        [SerializeField] private int m_EnergyUsage; // Расход энергии во время стрельбы стрельбы
        public int EnergyUsage => m_EnergyUsage;

        [SerializeField] private int m_AmmoUsage; // Расход патронов во время стрельбы
        public int AmmoUsage => m_AmmoUsage;

        [SerializeField] private AudioClip m_LaunchSFX; // Звук воспроизводимый при стрельбе
        public AudioClip LaunchSFX => m_LaunchSFX;

    }
}

