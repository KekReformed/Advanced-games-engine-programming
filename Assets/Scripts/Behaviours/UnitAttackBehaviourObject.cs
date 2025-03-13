using UnityEngine;

public abstract class UnitAttackBehaviourObject : ScriptableObject , IUnitAttackBehaviour
{
    public float range;
    public float attackInterval;
    [HideInInspector] public float attackCooldown;
    
    public abstract void Attack(GameObject unit, GameObject target);
}