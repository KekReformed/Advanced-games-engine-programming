using UnityEngine;

public interface IUnitAttackBehaviour : IUnitBehaviour
{
    public void Attack(GameObject unit, GameObject target); 
}