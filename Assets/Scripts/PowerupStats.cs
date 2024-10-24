using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        public enum EffectType // ������������ ��������� �������� Powerup
        {
            AddAmmo,
            AddEnergy,
            Invincibility // ������������
        }

        [SerializeField] private EffectType m_EffectType; // ��� ������������� ������� Powerup

        [SerializeField] private float m_Value; // �������� �������� ������������� ������� Powerup

        [SerializeField] private float m_InvincibilityDuration; // ����� �������� ������� ������������

        protected override void OnPickedUp(SpaceShip ship)
        {
            // ���� ��� ������������� ������� = AddAmmo, �� ������ AddAmmo �������� �������� ������������� ������� Powerup
            if (m_EffectType == EffectType.AddAmmo)
                ship.AddAmmo((int)m_Value);

            // ���� ��� ������������� ������� = AddEnergy, �� ������ AddEnergy �������� �������� ������������� ������� Powerup
            if (m_EffectType == EffectType.AddEnergy)
                ship.AddEnergy((int)m_Value);

            if (m_EffectType == EffectType.Invincibility)
                ship.EnableInvincibility(m_InvincibilityDuration);
        }
    }
}

