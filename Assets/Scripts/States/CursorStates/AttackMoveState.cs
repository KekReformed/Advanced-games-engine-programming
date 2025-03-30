using UnityEngine;
using UnityEngine.InputSystem;

namespace States.CursorStates
{
    public class AttackMoveState : ICursorState
    {
        InputAction _attackStateInput;
        
        public void Setup(PlayerManager manager)
        {
            _attackStateInput = manager.Input.actions.FindAction("Enter AttackMove State");
        }
        
        public bool CheckStateConditions(PlayerManager manager)
        {
            return _attackStateInput.triggered;
        }
        public void EnterState(PlayerManager manager)
        {
            Debug.Log("Attack move state entered!");
            PlayerCursor.Instance.image.color = Color.red;
        }
        
        public void UpdateState(PlayerManager manager)
        {
            if(!manager.CommandInput.triggered) return;
                
            foreach (Unit unit in manager.selectedUnits)
            {
                unit.MoveTarget = manager.HoveringOver.point;
                unit.SetState(new UnitStates.AttackMoveState());
            }
        }
        
        public void ExitState(PlayerManager manager)
        {
            return;
        }
    }
}

