using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerControlleer : MonoBehaviour
{
   // Store the input action sheet used for input 
   [SerializeField] private InputActionAsset InputActions; 

   //ACTIONS 
   private InputAction moveAction;
   private InputAction jumpAction;

   private Vector2 moveVector;

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
      InputActions.FindAction("Player").Disable();
   }

   private void Update() 
   {
      moveVector = moveAction.ReadValue<UnityEngine.Vector2>(); 

      if (jumpAction.WasPressedThisFrame())
      {
         
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
     Debug.Log("Jump My Bones!"); 
   }
}


