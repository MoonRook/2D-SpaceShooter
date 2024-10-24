using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCastAll : MonoBehaviour
{
    private void Update()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, transform.forward, 10);

        Debug.DrawLine(transform.position, transform.position + transform.forward * 10);

        for (int i = 0; i < hit.Length; i++)
            Debug.Log("RayCast All " + i + ": " + hit[i].collider.gameObject.name);
    }
}
