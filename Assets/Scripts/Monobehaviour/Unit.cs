using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    public IUnitState CurrentState { get; set; }
    public Vector3 MoveTarget { get; set; }
    public GameObject CurrentTarget { get;  set; }
    
    public UnitMovementBehaviourObject movementBehaviour;
    public UnitAttackBehaviourObject attackBehaviour;
    public UnitRange UnitRange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        attackBehaviour.ResetBehaviour();
        UnitRange = GetComponentInChildren<UnitRange>();
    }

    public void SetState(IUnitState state)
    {
        CurrentState?.ExitState(this);
        CurrentState = state;
        CurrentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState?.UpdateState(this);
    }
}
