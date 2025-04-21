using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using MovementState = States.CursorStates.MovementState;

public class PlayerManager : MonoBehaviour
{
    public List<Unit> selectedUnits = new List<Unit>();
    public PlayerInput Input { get; private set; }
    public InputAction CommandInput { get; private set; }
    public InputAction MoveInput { get; private set; }
    public Team playerTeam;
    protected Camera MainCam { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public RaycastHit HoveringOver { get; private set; }
    protected List<ICursorState> CursorStates { get; private set; } 
    public ICursorState CurrentState { get; private set; }
    
    ICursorState _previousState;
    
    public static PlayerManager Instance;
    
    [SerializeField] LayerMask raycastLayerMask;

    protected virtual void Awake()
    {
        Input = GetComponentInParent<PlayerInput>();
        CommandInput = Input.actions.FindAction("Left Click");
        MoveInput = Input.actions.FindAction("Right Click");
        MainCam = Camera.main;
        
        if (Instance != null)
        {
            Debug.LogError("Multiple player managers detected! there should only be 1!");
        }

        Instance = this;        
        
        CursorStates = CreateInstancesOfCursorStates();
    }

    protected virtual void Start()
    {
        SetState(new SelectionState());
    }

    protected void Update()
    {
        HandleInput();
        
        //Constantly try to set states this way states can decide what there activation conditions are, allowing extra states to be added outside of this class
        foreach (ICursorState cursorState in CursorStates)
        {
            if(!cursorState.CheckStateConditions(this)) continue;
            if(cursorState.GetType() == CurrentState.GetType()) continue;
            
            SetState(cursorState);
        }
        
        CurrentState?.UpdateState(this);
    }

    public GameObject GetHoveredOver()
    {
        return HoveringOver.collider.gameObject;
    }
    
    public void SetState(ICursorState state)
    {
        CurrentState?.ExitState(this);
        CurrentState = state;
        CurrentState.EnterState(this);
    }
    
    void HandleInput()
    {
        MousePosition = Mouse.current.position.ReadValue(); // Get mouse position
        Ray ray = MainCam.ScreenPointToRay(MousePosition); // Create a ray from the camera through the cursor
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 10000f, raycastLayerMask)) return;

        HoveringOver = hit;
    }
    
    List<ICursorState> CreateInstancesOfCursorStates()
    {
        Type targetType = typeof(ICursorState);
        List<ICursorState> instances = new List<ICursorState>();

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => targetType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (Type type in types)
        {
            ICursorState instance = (ICursorState)Activator.CreateInstance(type);
            instances.Add(instance);
            instance.Setup(this);
        }

        return instances;
    }
}
