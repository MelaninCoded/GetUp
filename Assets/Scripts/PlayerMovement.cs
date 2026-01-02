using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Initiating Variables
    [Header("Movement Settings")]
    public float speed = 2.49f;

    private Rigidbody rb;
    private PlayerControls controls; //Input Actions
    private Vector2 moveInput; //stores current movement speed

    private void Awake()
    {
        controls = new PlayerControls(); //initialize the controls

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
         
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //get the rb attached to player

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //convert the 2D input into a 3D movement vector
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);

        //Move the player using rb
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
