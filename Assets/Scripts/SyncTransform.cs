using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class SyncTransform : MonoBehaviour
    {
        [SerializeField] private Transform m_Targer;

        private void Update()
        {
            transform.position = new Vector3(m_Targer.position.x, m_Targer.position.y, transform.position.z);
        }
    }
}
