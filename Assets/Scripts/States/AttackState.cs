using UnityEngine;


public class AttackState : IUnitState
{
    public void EnterState(Unit unit)
    {
        return;
    }
    
    public void UpdateState(Unit unit)
    {
        if(unit.CurrentTarget == null) return;
        
        if (!unit.attackBehaviour.InRange(unit))
        {
            unit.movementBehaviour.Move(unit.gameObject, unit.CurrentTarget.transform.position);
            return;
        };
            
        unit.attackBehaviour.Attack(unit.gameObject, unit.CurrentTarget);
    }
    public void ExitState(Unit unit)
    {
        return;
    }
}