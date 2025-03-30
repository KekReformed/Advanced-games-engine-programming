using UnityEngine;

[CreateAssetMenu(fileName = "Basic Attack", menuName = "Attack Behaviours/Basic Attack")]
public class BasicUnitAttackBehaviour : UnitAttackBehaviourObject
{
    public override void Attack(GameObject unit, GameObject target)
    {
        base.Attack(unit, target);
        if(!CanAttack()) return;
        
        Debug.Log("Die!");
        ResetCooldown();
    }
}
