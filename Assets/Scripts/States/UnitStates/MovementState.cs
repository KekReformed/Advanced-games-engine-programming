﻿namespace States.UnitStates
{
    public class MovementState : IUnitState
    {
        public void EnterState(Unit unit)
        {
            return;
        }
        
        public virtual void UpdateState(Unit unit)
        {
            unit.movementBehaviour.Move(unit.gameObject, unit.MoveTarget);
        }
        
        public void ExitState(Unit unit)
        {
            return;
        }
    }
}


