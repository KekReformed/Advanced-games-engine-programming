using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
enum State 
{
    Attack, 
    Move
}

public class Unit : MonoBehaviour
{
    protected enum State 
    {
        Attack, 
        Move,
        None
    }
    
    PlayerInput _input;
    InputAction _mousePosition;
    InputAction _leftClick;
    InputAction _rightClick;
    
    Camera _mainCam;
    Vector3 _moveTarget;
    GameObject _currentTarget;
    
    [SerializeField] LayerMask raycastLayerMask;
    [SerializeField] UnitMovementBehaviourObject movementBehaviour;
    [SerializeField] UnitAttackBehaviourObject attackBehaviour;

    private State _state = State.None;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _input = GetComponentInParent<PlayerInput>();
        _mousePosition = _input.actions.FindAction("MousePosition");
        _leftClick = _input.actions.FindAction("Left Click");
        _rightClick = _input.actions.FindAction("Right Click");
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue(); // Get mouse position
        Ray ray = _mainCam.ScreenPointToRay(mousePosition); // Create a ray from the camera through the cursor
        RaycastHit hit;

        if (attackBehaviour.attackCooldown > 0) attackBehaviour.attackCooldown -= Time.deltaTime;

        if (_rightClick.triggered && Physics.Raycast(ray, out hit, 10000f,raycastLayerMask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Unit"))
            {
                _state = State.Attack;
                _currentTarget = hit.collider.gameObject;
            }
            else
            {
                _state = State.Move;
            }
            _moveTarget = hit.point;
        }

        switch (_state)
        {
            case State.Attack:
                if (Vector3.Distance(_currentTarget.transform.position, transform.position) > attackBehaviour.range)
                {
                    movementBehaviour.Move(gameObject, _currentTarget.transform.position);
                    break;
                };
                if(attackBehaviour.attackCooldown > 0) break;
                
                attackBehaviour.Attack(gameObject, _currentTarget);
                attackBehaviour.attackCooldown = attackBehaviour.attackInterval;
                
                break;
            
            case State.Move:
                movementBehaviour.Move(gameObject, _moveTarget);
                break;
        }
    }
}
