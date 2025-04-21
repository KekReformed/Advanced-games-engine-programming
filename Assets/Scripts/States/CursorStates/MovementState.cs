using UnityEngine;

namespace States.CursorStates
{
    public class MovementState : ICursorState
    {
        public void Setup(PlayerManager manager)
        {
            return;
        }
        
        public bool CheckStateConditions(PlayerManager manager)
        {
            return manager.MoveInput.triggered && manager.CurrentState.GetType() == typeof(UnitStates.MovementState);
        }
        
        public void EnterState(PlayerManager manager)
        {
            Debug.Log("Movement state entered");
            PlayerCursor.Instance.image.color = Color.white;
        }
        
        public void UpdateState(PlayerManager manager)
        {            
            if(!manager.MoveInput.triggered) return;
            
            foreach (Unit unit in manager.selectedUnits)
            {
                unit.MoveTarget = manager.HoveringOver.point;
                
                unit.SetState(new UnitStates.MovementState());
            }
        }
        
        public void ExitState(PlayerManager manager)
        {
            return;
        }
    }
}
