using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit; // ������� ��� � ���������� ���� ������

        // ��������� ����
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10);

        // ������ ����� ������ ����, ����������� ����, ��������� ����� � ���������� �������� true
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10) == true)
        {
            // ����� ��������� � ������� 
            Debug.Log("RayCast: " + hit.collider.gameObject.name);
        }          
    }
}
