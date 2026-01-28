using UnityEngine;
using UnityEngine.InputSystem;

public class Camara : MonoBehaviour
{
    public Transform player;
    
    [Header("Mouse")]
    public float mouseSensivity;
    public bool invertY = false;
    
    [Header("Camara")]
    public float distance = 4f;
    public float height = 1.6f;
    
    public float minY = -35f;
    public float maxY = 70f;

    private float yaw;
    private float pitch;
    
    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        
        inputActions.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        
        inputActions.Player.Look.canceled += _ => Look(Vector2.zero);
    }
    
    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Look(Vector2 mouse)
    {
       yaw += mouse.x * mouseSensivity; 
       
       float invert = invertY ? -1 : 1;
       pitch += mouse.y * mouseSensivity * invert;
       
       pitch = Mathf.Clamp(pitch, minY, maxY);
    }
    
    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 position =
            player.position - rotation * Vector3.forward * distance + Vector3.up * height;

        transform.position = position;
        transform.rotation = rotation;
    }
}
