using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(MeshRenderer))] // ��������� ���������
    public class BackgroundElement : MonoBehaviour
    {
        [Range(0.0f, 4.0f)]

        [SerializeField] private float m_ParallaxPower; // ���� ��������� �������

        [SerializeField] private float m_TextureScale; // ������ ��������

        private Material m_QuadMaterial; // ������ �� ��������
        private Vector2 m_InitialOffset; // ����������� ����� �������

     
        /// <summary>
        /// �������� ������ �� �������� � �������� ��������� ����� ������� � ��������� ����������
        /// </summary>
        private void Start()
        {
            m_QuadMaterial = GetComponent<MeshRenderer>().material;
            m_InitialOffset = UnityEngine.Random.insideUnitCircle;

            // � ������ �� �������� ������ ������ ������� = ��������� ������ 2 * ������ ��������
            m_QuadMaterial.mainTextureScale = Vector2.one * m_TextureScale; 
        }
        /// <summary>
        /// ������� ������ ������� ������ ����������� ����� ������� � �������� ������
        private void Update()
        {
            Vector2 offset = m_InitialOffset;

            // ����� ������ � += ������� �� � / ��������� ������ � / ���� ��������� �������
            offset.x += transform.position.x / transform.localScale.x / m_ParallaxPower;

            // ����� ������ y += ������� �� y / ��������� ������ y / ���� ��������� �������
            offset.y += transform.position.y / transform.localScale.y / m_ParallaxPower;

            // � ������ �� �������� ������ ������ ������� ������� = ����� �������
            m_QuadMaterial.mainTextureOffset = offset;
        }

    }
}
