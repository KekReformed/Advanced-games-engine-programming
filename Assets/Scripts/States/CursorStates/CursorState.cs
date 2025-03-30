using UnityEngine;

public abstract class CursorState : ICursorState
{
    public abstract void Setup(PlayerManager manager);
    public abstract bool CheckStateConditions(PlayerManager manager);
    public abstract void EnterState(PlayerManager manager);
    public abstract void UpdateState(PlayerManager manager);
    public abstract void ExitState(PlayerManager manager);
}
