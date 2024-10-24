using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera; // ��, ��� ������ �� ��������
        
        [SerializeField] private Transform m_Target; // ������ ��������, ����

        [SerializeField] private float m_InterpolationLianer; // �������� ������������

        [SerializeField] private float m_InterpolationAngular; // �������� ������� ������������

        [SerializeField] private float m_CameraZOffset; // �������� �� ��� Z

        [SerializeField] private float m_ForwardOffset; // �������� �� ����������� ��������

        private void FixedUpdate()
        {
            if (m_Target == null || m_Camera == null) return; // �������� �� ������� ������� �������� � ������

            Vector2 camPos = m_Camera.transform.position; // ������ ������� ������ = ������� ������

            // ������� ������� = ������� ����
            // + ����������� �������� ���� * �������� �� ����������� �������� 
            Vector2 targePos = m_Target.position + m_Target.transform.up * m_ForwardOffset; 

            // ����� ������� ������ = ������������ ������� 2 (����� ������� ������� ������, ������� ������� ������,
            // ��������� ������������ * �� ����� ���������� �����)
            Vector2 newcamPos = Vector2.Lerp(camPos, targePos, m_InterpolationLianer * Time.deltaTime);

            // ������� ������ = ����� ������ 3 (����� ������� ������ �� � � y, �������� �� ��� Z)
            m_Camera.transform.position = new Vector3(newcamPos.x, newcamPos.y, m_CameraZOffset); 

            // �������� �� �������� ��������
            if (m_InterpolationAngular > 0)
            {
                // ������� ������ = ������������ ����������� (������� ��������� �������� ������, ������� ������� �������� �������,
                // �������� ������������ * �� ����� ���������� �����)
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation,
                  m_Target.rotation, m_InterpolationAngular * Time.deltaTime);
            }
        }
        public void SetTarget(Transform newTarget)
        {
            m_Target = newTarget; // ������ ����� ���������� ��� ��������
        }
    }
}
