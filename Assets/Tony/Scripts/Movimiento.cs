using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    [Header("Referencias")]
    public Transform camera;
    private Vector2 moveInput;
    private float currentVelocity;
    private bool isSprinting;
    
    private CharacterController controller;
    private PlayerInputActions inputActions;
    
    [Header("Movimiento")]
    public float moveSpeed;
    public float sprintSpeed;
    public float rotationSmooth = 0.1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        inputActions = new PlayerInputActions();
        
        
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        
        inputActions.Player.Sprint.performed += _ => isSprinting = true;
        inputActions.Player.Sprint.canceled += _ => isSprinting = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
        forward.y = 0;
        right.y = 0;
        right.Normalize();
        forward.Normalize();
        
        Vector3 direction = right * moveInput.x + forward * moveInput.y;
        if (direction.magnitude > 1f)
        {
            direction.Normalize();
        }
        
        float speed = isSprinting ? sprintSpeed : moveSpeed;
        controller.Move(direction * (speed * Time.deltaTime));
        
        if (direction.magnitude > 0.1f)
        {
            float targetAngle =
                Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float smoothAngle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref currentVelocity,
                rotationSmooth
            );

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
        }
        
        
    }
}
