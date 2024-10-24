using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InitEvent : MonoBehaviour
{
    public UnityEvent OnClik;

    private void OnMouseDown()
    {
        OnClik?.Invoke();
    }
}
