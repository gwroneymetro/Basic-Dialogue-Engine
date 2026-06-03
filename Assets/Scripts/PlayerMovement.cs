using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{

    ///vars
    /// 
    /// 
    /// 
    ///Input Stuff 
    [SerializeField] private InputActionAsset InputActions; //put InputSystem_Actions in here

    //Input Actions
    private InputAction moveInput;    
    private InputAction turnInput;    
    private InputAction jumpInput;    


    /// Player movement force variables
    public float playerWalkSpeed;
    public float playerTurnSpeed;
    public float jumpForce;
    private Vector2 groundMoveValue; ///how much the player moves forward and back
    private Vector2 turnMoveValue; ///how much the player rotates looking

    //Rigidbody
    [SerializeField] private Rigidbody rb; //drag the PlayerObj rigidbody into this field

    ///Check to see if you're touching the ground while jumping
    public LayerMask groundMask;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //go into your InputActions asset and enable the
        //"Player" action map
        InputActions.FindActionMap("Player").Enable();
        moveInput = InputSystem.actions.FindAction("Move");
        jumpInput = InputSystem.actions.FindAction("Jump");
        turnInput = InputSystem.actions.FindAction("Look");
        

        ///Lock the Cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;




    }

    private void OnDisable()
    {
        //Disable the Dialogue actionmap when you're not in Dialogue
        InputActions.FindActionMap("Player").Disable();                //point the action variables to the correct actions in the actionmap

        ///Unock the Cursor from the center of the screen and make it visible
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;


    }

    // Update is called once per frame
    void Update()
    {
        //Read the value off the input and assign it to the groundMove vector2
        groundMoveValue = moveInput.ReadValue<Vector2>();
        turnMoveValue = turnInput.ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        ///Add force to the rigidbody in the direction and amount 
        /// of groundMoveValue * the variable playerWalkSpeed
        /// to X and Z, 
        /// while Y
        rb.AddRelativeForce(groundMoveValue.x * playerWalkSpeed, 0, groundMoveValue.y * playerWalkSpeed);



        rb.AddRelativeTorque(0,turnMoveValue.x * playerTurnSpeed,0, ForceMode.VelocityChange);

    }

}
