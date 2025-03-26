using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitRange : MonoBehaviour
{
    public List<Unit> UnitsInRange { get; private set; } = new List<Unit>();

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Unit otherUnit = other.GetComponent<Unit>();
        
        if (otherUnit == null) return;
        UnitsInRange.Add(otherUnit);
    }

    void OnTriggerExit(Collider other)
    {
        Unit otherUnit = other.GetComponent<Unit>();
        
        if (otherUnit == null) return;
        UnitsInRange.Remove(otherUnit);
    }
}
