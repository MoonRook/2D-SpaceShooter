using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit; // Бросает луч и возвращает один объект

        // Отрисовка луча
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10);

        // Задает точку старта луча, направление луча, дистанцию лучаи и возвращает параметр true
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10) == true)
        {
            // Вывод сообщения в консоль 
            Debug.Log("RayCast: " + hit.collider.gameObject.name);
        }          
    }
}
