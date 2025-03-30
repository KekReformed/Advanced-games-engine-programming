using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitRange : MonoBehaviour
{
    public List<Unit> UnitsInRange { get; private set; } = new List<Unit>();

    void OnTriggerEnter(Collider other)
    {
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
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetComponentInParent<Unit>().attackBehaviour.range);
    }
}
