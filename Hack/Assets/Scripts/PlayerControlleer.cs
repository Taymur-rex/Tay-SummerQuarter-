using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlleer : MonoBehaviour
{
    // Store the input action sheet used for input
    [SerializeField] private InputActionAsset InputActions;

    // ACTIONS
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveInput;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 1f;

    // NEW - Mouse Aim
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask aimLayer;

    // COMPONENTS
    [SerializeField] private Rigidbody rb;

    // PLAYER SETTINGS
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player")?.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        HandleRotation();

        if (jumpAction.WasPressedThisFrame())
        {
            HandleJump();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = transform.forward * moveInput.y +
                                transform.right * moveInput.x;

        moveDirection.Normalize();

        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, aimLayer))
        {
            Vector3 lookDirection = hit.point - transform.position;
            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }

    private void HandleJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}