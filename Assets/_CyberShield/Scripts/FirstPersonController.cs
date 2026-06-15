using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    private PlayerControls controls;
    private CharacterController controller;
    
    private Vector2 moveInput;
    private Vector2 lookInput;

    public float speed = 10f;
    public float mouseSensitivity = 20f;
    
    public Transform cameraTransform;
    private float xRotation = 0f;

    private void Awake()
    {
        controls = new PlayerControls();
        controller = GetComponent<CharacterController>();

        // Set up our input callbacks for movement and looking around
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable() { controls.Player.Enable(); }
    private void OnDisable() { controls.Player.Disable(); }

    private void Start()
    {
        // Locks the cursor to the center of the screen and makes it invisible for a true first-person experience
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MovePlayer();
        LookAround();
    }

    private void MovePlayer()
    {
        // Calculate movement direction based on input and player orientation
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        
        // Apply gravity to keep the player grounded (you can expand this with jumping later!)
        move.y = -9.81f; 

        controller.Move(move * speed * Time.deltaTime);
    }

    private void LookAround()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevents the player from looking too far up or down

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}