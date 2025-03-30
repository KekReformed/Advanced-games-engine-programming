namespace States.UnitStates 
{
    public class AttackMoveState : MovementState
    {
        public override void UpdateState(Unit unit)
        {
            base.UpdateState(unit);
            if(unit.UnitRange.UnitsInRange.Count > 0) unit.SetState(new AttackState(unit.UnitRange.UnitsInRange[0].gameObject));
        }
    }
}
