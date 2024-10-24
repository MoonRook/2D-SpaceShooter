using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        public enum EffectType // Перечисление возможных эффектов Powerup
        {
            AddAmmo,
            AddEnergy,
            Invincibility // Неуязвимость
        }

        [SerializeField] private EffectType m_EffectType; // Тип прибавляемого эффекта Powerup

        [SerializeField] private float m_Value; // Числовое значение прибавляемого эффекта Powerup

        [SerializeField] private float m_InvincibilityDuration; // время действия эффекта неуязвимости

        protected override void OnPickedUp(SpaceShip ship)
        {
            // Если тип прибавляемого эффекта = AddAmmo, то задаем AddAmmo числовое значение прибавляемого эффекта Powerup
            if (m_EffectType == EffectType.AddAmmo)
                ship.AddAmmo((int)m_Value);

            // Если тип прибавляемого эффекта = AddEnergy, то задаем AddEnergy числовое значение прибавляемого эффекта Powerup
            if (m_EffectType == EffectType.AddEnergy)
                ship.AddEnergy((int)m_Value);

            if (m_EffectType == EffectType.Invincibility)
                ship.EnableInvincibility(m_InvincibilityDuration);
        }
    }
}

