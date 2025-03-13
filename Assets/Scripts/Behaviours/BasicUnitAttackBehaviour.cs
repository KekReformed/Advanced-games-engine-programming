using UnityEngine;

[CreateAssetMenu(fileName = "Basic Attack", menuName = "Attack Behaviours/Basic Attack")]
public class BasicUnitAttackBehaviour : UnitAttackBehaviourObject
{
    public override void Attack(GameObject unit, GameObject target)
    {
        Debug.Log("Die!");
    }
}
