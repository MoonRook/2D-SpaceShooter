using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof (CircleCollider2D))] // ��������� ������ �� ��������� CircleCollider2D
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float m_Force; // ���� ����������
        [SerializeField] private float m_Radius; // ������ �������� ���� ����������

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return; // ��������� collision �� ������� ���������� Rigidbody

            Vector2 dir = transform.position - collision.transform.position; // ���������� ������ 2 = ������� ���������� - ������� ���������� ��������

            float dist = dir.magnitude; // ��������� = ��������� ���������� ������ 2

            if (dist < m_Radius)// ���� ��������� ������ ������� �������� ���� ����������
            {
                // ������� ������� ������ 2 = ��������������� ����������� * ���� ���������� * (��������� / ������ �������� ���� ����������)
                Vector2 force = dir.normalized * m_Force * (dist / m_Radius);

                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }

        }
    #if UnityEditor
        
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
    # endif 
    }
}
