using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float m_Lifetime; // Общее время жизни

        private float m_Timer; // Прожитое время

        private void Update()
        {
            // Если прожитое время меньше общего время жизни, то прибавляем к прожитому времени частоту обновления кадра
            if (m_Timer < m_Lifetime) 
                m_Timer += Time.deltaTime;
            else
                // Если прожитое время больше общего время жизни, то уничтожаем объект
                Destroy(gameObject);
        }
    }
}

