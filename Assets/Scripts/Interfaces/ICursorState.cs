using UnityEngine;

public interface ICursorState
{
    void Setup(PlayerManager manager);
    bool CheckStateConditions(PlayerManager manager);
    void EnterState(PlayerManager manager);
    void UpdateState(PlayerManager manager);
    void ExitState(PlayerManager manager);
}
