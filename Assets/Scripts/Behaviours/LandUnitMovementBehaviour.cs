using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Land movement", menuName = "Movement Behaviours/Land Movement")]
public class LandUnitMovementBehaviour : UnitMovementBehaviourObject
{
    [SerializeField] float speed;
    Node[] _path;
    int _pathIndex = 1;
    
    public override void Move(GameObject unit, Vector3 target)
    {
        Vector3 moveVector;
        
        if (Physics.Linecast(unit.transform.position, target, Settings.instance.pathfindingLayerMask))
        {
            if(_path == null) _path = PathfindingManager.FindPath(unit, target);

            Vector3 tempTarget = _path[_pathIndex].Position;
            
            moveVector = (unit.transform.position - tempTarget).normalized;

            Vector3 pathDifference = _path[_pathIndex - 1].Position - _path[_pathIndex].Position;

            float expectedXSign = Mathf.Sign(pathDifference.x);
            float expectedZSign = Mathf.Sign(pathDifference.z);

            if (Mathf.Sign(moveVector.x) != expectedXSign && Mathf.Sign(moveVector.z) != expectedZSign) _pathIndex++;
            else if (Vector3.Distance(unit.transform.position, tempTarget) < 1.5f) _pathIndex++;
        }
        else
        {
            moveVector = (unit.transform.position - target).normalized;

            _path = null;
            _pathIndex = 1;
        } 
        
        moveVector = new Vector3(moveVector.x, 0f, moveVector.z);
        unit.transform.position -= moveVector * Time.deltaTime * speed;
    }
}