using UnityEngine;
using UnityEngine.InputSystem;

namespace States.CursorStates
{
    public class AttackState : ICursorState
    {
        
        public void Setup(PlayerManager manager)
        {
            
        }
        
        public bool CheckStateConditions(PlayerManager manager)
        {
            return manager.HoveringOver.collider && manager.GetHoveredOver().layer == LayerMask.NameToLayer("Unit");
        }
        
        public void EnterState(PlayerManager manager)
        {
            Debug.Log("Attack state entered");
            PlayerCursor.Instance.image.color = Color.red;
        }
        
        public virtual void UpdateState(PlayerManager manager)
        {
            if(!manager.HoveringOver.collider || manager.GetHoveredOver().layer != LayerMask.NameToLayer("Unit")) manager.SetState(new MovementState());
            if(!manager.MoveInput.triggered) return;
            
            foreach (Unit unit in manager.selectedUnits)
            {
                unit.SetState(new UnitStates.AttackState(manager.HoveringOver.collider.gameObject));
            }
            
            manager.SetState(new MovementState());
        }

        public void ExitState(PlayerManager manager)
        {
            return;
        }
    }
}

