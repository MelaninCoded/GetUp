using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("Settings")]
    public float mouseSensitivity = 100f;

    [Header("References")]
    public Transform playerBody;    // Player object (rotates horizontally)
    public Transform playerCamera;  // Camera (rotates vertically)

    private PlayerControls controls;
    private Vector2 lookInput;
    private float xRotation = 0f;

    void Awake()
    {
        controls = new PlayerControls();

        // Look action
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        // Pause action (unlock cursor)
        controls.Player.Pause.performed += ctx => UnlockCursor();
    }

    private void OnEnable()
    {
        if (controls != null)
            controls.Player.Enable();
    }

    private void OnDisable()
    {
        if (controls != null)
            controls.Player.Disable();
    }

    void Start()
    {
        // Lock cursor at start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse movement scaled by sensitivity
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Vertical rotation (camera)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (player body)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
