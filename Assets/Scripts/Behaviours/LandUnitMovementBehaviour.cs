using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Land movement", menuName = "Movement Behaviours/Land Movement")]
public class LandUnitMovementBehaviour : UnitMovementBehaviourObject
{
    [SerializeField] float speed;
    
    public override void Move(GameObject unit, Vector3 target)
    {
        Vector3 moveVector = (unit.transform.position - target).normalized;
        moveVector = new Vector3(moveVector.x, 0f, moveVector.z);
        unit.transform.position -= moveVector * Time.deltaTime * speed;
    }
}