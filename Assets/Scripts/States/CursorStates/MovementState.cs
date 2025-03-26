namespace States.CursorStates
{
    public class MovementState : ICursorState
    {
        public void Setup()
        {
            return;
        }
        
        public bool CheckStateConditions()
        {
            return PlayerManager.Instance.CommandInput.triggered;
        }
        
        public void EnterState()
        {
            return;
        }
        
        public void UpdateState()
        {            
            if(!PlayerManager.Instance.CommandInput.triggered) return;
            
            foreach (Unit unit in PlayerManager.Instance.selectedUnits)
            {
                unit.MoveTarget = PlayerManager.Instance.MouseHit.point;
                unit.SetState(new UnitStates.MovementState());
            }
        }
        
        public void ExitState()
        {
            return;
        }
    }
}
