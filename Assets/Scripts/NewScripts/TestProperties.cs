using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossProperties", menuName = "Properties/Boss")]
public class TestProperties : ScriptableObject
{
    [SerializeField] private int Health;

    [SerializeField] private int Energyh;
}  


