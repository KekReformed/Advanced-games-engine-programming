using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    public LayerMask pathfindingLayerMask;
    public static Settings instance;

    void OnValidate()
    { 
        instance = this;
    }
}
