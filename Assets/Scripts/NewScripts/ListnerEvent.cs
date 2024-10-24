using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListnerEvent : MonoBehaviour
{
    public InitEvent initEvent;

    private void Start()
    {
        initEvent.OnClik.AddListener(OnClickEvent);
    }

    private void OnClickEvent()
    {
        Debug.Log("Нажал");
    }
}
