using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode; // ����� ������ �������
        public TurretMode Mode => m_Mode;

        [SerializeReference] private TurretProperties m_TurretProperties; // ������ �� ��������� ������ TurretProperties
        
        private float m_RefireTimer; // ������ ���������� ��������

        public bool CanFire => m_RefireTimer <= 0; // ������� ��������. ���� ������ ���������� �������� <= 0, �� ���������� true ����� ���������� false

        private SpaceShip m_Ship; // ������������ ������ �� ������� ��������

        /// <summary>
        /// Unity Event
        /// </summary>
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }
        private void Update()
        {
           if (m_RefireTimer > 0) // ���� ������ ���������� �������� > 0, �� �������� �� ���� ������� ���������� �����
                m_RefireTimer -= Time.deltaTime;
        }

        /// <summary>
        /// Public API
        /// </summary>
        public void Fire()
        { 
            if (m_TurretProperties == null) return;
            
            if (m_RefireTimer > 0) return;

            // �������� ����� ������ �������. ���� ������� = 0, �� ������� �� ������ � �� ������� ������
            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) 
                return;

            // �������� ����� ������ �������. ���� ���������� �������� = 0, �� ������� �� ������ � �� ������� ������
            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) 
                return;

            // ������� ��������� ������� ������� ProjectilePrefab
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();

            projectile.transform.position = transform.position; // ������ ��������� ���������� ������� �������

            projectile.transform.up = transform.up; // ������ �������� ���������� ������� �������

            projectile.SetParentShooter(m_Ship); // ������������� �������� �� ������ ����
            
            m_RefireTimer = m_TurretProperties.RateOfFire; // ������ ���������� �������� = c��������������� ������ � �������

            
            
            {
                //SFX  ��� ��� ������� ����� ���������������� ��� ��������
            }

        }
        /// <summary>
        /// PowerUp
        /// </summary>
        /// <param name="props"></param>
        public void AssignLoadout(TurretProperties props)
        {
            // ��������� ������ ������ (��������� ��� ���������������) �� ����������� �������� PowerUp
            if (m_Mode != props.Mode) return; 

            m_RefireTimer = 0; // ���������� ������ �� 0, ��� ������� PowerUp

            m_TurretProperties = props;
        }
    }
}
