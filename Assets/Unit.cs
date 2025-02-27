using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    PlayerInput _input;
    InputAction _mousePosition;
    Camera mainCam;
    [SerializeField] LayerMask raycastLayerMask; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _mousePosition = _input.actions.FindAction("MousePosition");
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue(); // Get mouse position
        Ray ray = mainCam.ScreenPointToRay(mousePosition); // Create a ray from the camera through the cursor
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000f,raycastLayerMask))
        {
            Debug.Log(hit.point);
            Vector3 moveVector = (transform.position - hit.point).normalized;
            moveVector = new Vector3(moveVector.x, 0f, moveVector.z);
            transform.position -= moveVector * 0.2f;
        }
    }
}
