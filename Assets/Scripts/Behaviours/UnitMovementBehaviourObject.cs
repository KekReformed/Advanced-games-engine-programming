using UnityEngine;

public abstract class UnitMovementBehaviourObject : ScriptableObject , IUnitMovementBehaviour
{
    public abstract void Move(GameObject unit, Vector3 target);
}

