using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerControlleer : MonoBehaviour
{
   // Store the input action sheet used for input 
   [SerializeField] private InputActionAsset InputActions; 

   //ACTIONS 
   private InputAction moveAction;
   private InputAction jumpAction;

   private Vector2 moveInput;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private float groundCheckDistance = 1f;

   // COMPONENTS
   [SerializeField] private Rigidbody rb; 

   // PLAYER SETTINGS 
   [SerializeField] private float moveSpeed = 5f;

   [SerializeField] private float jumpForce = 5f; 

   // Awake() is called once only 
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
      InputActions.FindAction("Player")?.Disable();
   }

   private void Update() 
   {
      moveInput = moveAction.ReadValue<UnityEngine.Vector2>(); 

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
      UnityEngine.Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x; 

      moveDirection.Normalize();

      rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime); 
   }

   private void HandleJump()
   {
      if (IsGrounded())
      {
        rb.AddForce(UnityEngine.Vector3.up * jumpForce, ForceMode.Impulse);
      }
   }


  private bool IsGrounded()
  {
    return Physics.Raycast(transform.position, UnityEngine.Vector3.down, groundCheckDistance, groundLayer);
  }

}


