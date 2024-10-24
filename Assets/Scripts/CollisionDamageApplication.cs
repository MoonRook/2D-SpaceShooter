using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamageApplication : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float m_VelocityDamageModifier;

        [SerializeField] private float m_DamageConstat;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;
            
            var destructable = transform.root.GetComponent<Destructible>();

            if (destructable != null)
            {
                destructable.ApplyDamage((int)m_DamageConstat + 
                    (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
            }
        }
    }
        
}

