using SpaceShooter;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupInvincibility : Powerup
    {
        [SerializeField] private float m_InvincibilityDuration = 20f; // ������������ ������������ � ��������

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.EnableInvincibility(m_InvincibilityDuration);
        }
    }
}
