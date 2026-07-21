using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlleer : MonoBehaviour
{
    // Store the input action sheet used for input
    [SerializeField] private InputActionAsset InputActions;

    // ACTIONS
    private InputAction moveAction;
    //private InputAction jumpAction;
    private InputAction shootAction;

    private Vector2 moveInput;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 1f;

    // NEW - Mouse Aim
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask aimLayer;

    // COMPONENTS
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunBarrel;
    [SerializeField] private Transform bulletsParent;
    [SerializeField] private Animator animator;

    // PLAYER SETTINGS
    [SerializeField] private float moveSpeed = 5f;
    //[SerializeField] private float jumpForce = 5f;

    [SerializeField] private float fireRate = 5f; // shots per second

    private float nextFireTime;
    private bool isDead = false;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        //jumpAction = InputSystem.actions.FindAction("Jump");
        shootAction = InputSystem.actions.FindAction("Shoot");
        isDead = false;
      
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

        /*if (jumpAction.WasPressedThisFrame())
        {
            HandleJump();
        }*/

        if (shootAction.IsPressed())
        {
            HandleShooting();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleShooting()
    {
        if (isDead) return;

      // Spawn a bullet at the barrel of his gun
      if (Time.time >= nextFireTime)
      {
         nextFireTime = Time.time + (1f / fireRate);

         // Spawn bullet here
         Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation, bulletsParent);
      }
    }

    private void HandleMovement()
{
    if (isDead) return;

    // Move relative to the world, not the player's facing direction.
    Vector3 moveDirection = Vector3.forward * moveInput.y +
                            Vector3.right * moveInput.x;

    moveDirection.Normalize();

    rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
}

    private void HandleRotation()
    {
        if (isDead) return;

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

   /*private void HandleJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }*/

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("GAME OVER!");
       // trigger death animation
      animator.SetTrigger("Death");
       // stop player movement
       // trigger game over UI
    }
}