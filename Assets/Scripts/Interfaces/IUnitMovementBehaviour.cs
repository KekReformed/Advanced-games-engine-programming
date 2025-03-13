using UnityEngine;

public interface IUnitMovementBehaviour : IUnitBehaviour
{
    public void Move(GameObject unit, Vector3 target);
}