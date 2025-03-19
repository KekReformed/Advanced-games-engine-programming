using System;
using UnityEngine;

public abstract class UnitAttackBehaviourObject : ScriptableObject , IUnitAttackBehaviour
{
    public float range;
    public float attackInterval;
    public float AttackCooldown { get; set; }

    float _lastCalledTime;

    public void ResetBehaviour()
    {
        _lastCalledTime = 0;
        AttackCooldown = 0;
    }

    public virtual void Attack(GameObject unit, GameObject target)
    {
        AttackCooldown += _lastCalledTime - Time.time;
        _lastCalledTime = Time.time;
    }
    
    protected bool CanAttack()
    {
        return AttackCooldown <= 0;
    }

    protected void ResetCooldown()
    {
        AttackCooldown = attackInterval;
    }

    public bool InRange(Unit unit)
    {
        return Vector3.Distance(unit.CurrentTarget.transform.position, unit.transform.position) < unit.attackBehaviour.range;
    }
}