using UnityEngine;

public interface ICursorState
{
    void Setup();
    bool CheckStateConditions();
    void EnterState();
    void UpdateState();
    void ExitState();
}
