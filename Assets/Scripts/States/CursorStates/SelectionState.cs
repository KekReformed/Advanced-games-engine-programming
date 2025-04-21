using States.CursorStates;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionState : CursorState
{
    InputAction _selectionStateInput;
    
    public override void Setup(PlayerManager manager)
    {
        _selectionStateInput = manager.Input.actions.FindAction("Enter Selection State");
    }
    public override bool CheckStateConditions(PlayerManager manager)
    {
        return _selectionStateInput.triggered;
    }
    public override void EnterState(PlayerManager manager)
    {
        PlayerCursor.Instance.image.color = Color.blue;
        Debug.Log("Selection State");
        manager.selectedUnits.Clear();
    }
    public override void UpdateState(PlayerManager manager)
    {
        if (!manager.CommandInput.triggered) return;

        if (manager.GetHoveredOver().layer != LayerMask.NameToLayer("Unit")) return;
        
        Unit hoveredUnit = manager.GetHoveredOver().GetComponent<Unit>();
        
        if(hoveredUnit.team.teamName != manager.playerTeam.teamName) return;
        
        manager.selectedUnits.Add(hoveredUnit);
        manager.SetState(new MovementState());
    }
    public override void ExitState(PlayerManager manager)
    {
        return;
    }
}
