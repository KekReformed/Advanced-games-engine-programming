using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    PlayerInput _input;
    InputAction _rightClick;
    
    Camera _mainCam;
    
    public IUnitState CurrentState { get; private set; }
    public Vector3 MoveTarget { get; private set; }
    public GameObject CurrentTarget { get; private set; }
    
    [SerializeField] LayerMask raycastLayerMask;
    public UnitMovementBehaviourObject movementBehaviour;
    public UnitAttackBehaviourObject attackBehaviour;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _input = GetComponentInParent<PlayerInput>();
        _rightClick = _input.actions.FindAction("Right Click");
        _mainCam = Camera.main;
        CurrentState = new MovementState();
        attackBehaviour.ResetBehaviour();
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
        HandleInput();
        CurrentState?.UpdateState(this);
    }

    void HandleInput()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue(); // Get mouse position
        Ray ray = _mainCam.ScreenPointToRay(mousePosition); // Create a ray from the camera through the cursor
        RaycastHit hit;

        if (!_rightClick.triggered || !Physics.Raycast(ray, out hit, 10000f, raycastLayerMask)) return;
        
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            CurrentTarget = hit.collider.gameObject;
            SetState(new AttackState());
        }
        else
        {            
            MoveTarget = hit.point;
            SetState(new MovementState());
        }
    }
}
