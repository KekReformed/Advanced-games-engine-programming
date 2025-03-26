using UnityEngine;
using UnityEngine.InputSystem;

namespace States.CursorStates
{
    public class AttackState : ICursorState
    {
        InputAction _attackStateInput;
        
        public void Setup()
        {
            _attackStateInput = PlayerManager.Instance.Input.actions.FindAction("Enter Attack State");
        }
        
        public bool CheckStateConditions()
        {
            return _attackStateInput.triggered;
        }
        
        public void EnterState()
        {
            Debug.Log("Attack State Entered;");
        }
        
        public void UpdateState()
        {
            if(!PlayerManager.Instance.CommandInput.triggered) return;
            if(PlayerManager.Instance.MouseHit.collider.gameObject.layer != LayerMask.NameToLayer("Unit")) return;
            
            foreach (Unit unit in PlayerManager.Instance.selectedUnits)
            {
                unit.CurrentTarget = PlayerManager.Instance.MouseHit.collider.gameObject;
                unit.SetState(new UnitStates.AttackState());
            }
            
            PlayerManager.Instance.SetState(new MovementState());
        }

        public void ExitState()
        {
            return;
        }
    }
}

