using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Powerup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.GetComponentInParent<SpaceShip>();

            if (ship != null && Player.Instance.ActiveShip)
            {
                OnPickedUp(ship);

                Destroy(gameObject);
            }
        }
        protected abstract void OnPickedUp(SpaceShip ship);
    }
}

