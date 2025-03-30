using UnityEngine;

namespace States.UnitStates
{
    public class AttackState : IUnitState
    {
        GameObject _target;

        public AttackState(GameObject target)
        {
            _target = target;
        }
        
        public void EnterState(Unit unit)
        {
            
        }
        
        public void UpdateState(Unit unit)
        {
            if(_target == null) return;
            
            if (!unit.attackBehaviour.InRange(unit, _target))
            {
                unit.movementBehaviour.Move(unit.gameObject, _target.transform.position);
                return;
            };
                
            unit.attackBehaviour.Attack(unit.gameObject, _target);
        }
        public void ExitState(Unit unit)
        {
            return;
        }
    }
}
