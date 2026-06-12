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

    [SerializeField]private GameObject glideObj; //If this is active, player glides
                                                //with zero downward force
    [SerializeField]private GameObject dashObj; //If this is active, player moves very fast                                               

    [SerializeField]private GameObject ThirdPersonCam; //track this for switching movement modes 

    /// Player movement force variables
    public float playerWalkSpeed;
    public float playerDashSpeed;
    public float playerSpeed;
    public float playerTurnSpeed;
    public float playerDownwardForce;
    public float jumpForce;
    private Vector2 groundMoveValue; ///how much the player moves forward and back
    private Vector2 turnMoveValue; ///how much the player rotates looking
    [SerializeField]private bool isJumping; //tells the physics engine we're jumping this frame


    //Rigidbody
    [SerializeField] private Rigidbody rb; //drag the PlayerObj rigidbody into this field

    ///Check to see if you're touching the ground while jumping
    public LayerMask groundMask;
    [SerializeField]private bool isGrounded;

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

        //check for jump input
        if((jumpInput.WasPressedThisFrame()) && (isGrounded == true))
        {
            isJumping = true;

        }

        if(isGrounded == false) 
        {
            if(glideObj.activeInHierarchy == false)//is the Glide obj active?
            {
            playerDownwardForce = -.3f; //If in the air, force the player to the ground quicker
            rb.AddForce(0, playerDownwardForce, 0, ForceMode.Impulse);
            }
            else
            {
            playerDownwardForce = 0; //If in the air with Glide on, no extra downward force
            }
        }

        if(dashObj.activeInHierarchy == false)///If dash object is inactive
        {
            playerSpeed = playerWalkSpeed; ///speed is walk speed
        }
        else //if dash object is active, speed is walk+dash speed
        {
            playerSpeed = playerWalkSpeed + playerDashSpeed;
        }
    }

    private void FixedUpdate()
    {

        if(isJumping == true)
        {
            //add the jump force to the Y axis and don't add any force to x or z
            //because you're not on the ground and shouldn't be able to turn
            rb.AddRelativeForce(0, jumpForce, 0, ForceMode.Impulse);
            isJumping = false;
        }
        else
        {
        playerDownwardForce = 0; //Don't need downward force if we're on the ground
        }






        if(ThirdPersonCam.activeInHierarchy == true) ///check which camera's on
        {
            ///Add force to the rigidbody in the direction and amount 
            /// of groundMoveValue * the variable playerSpeed
            /// to X and Z, 
            /// while Y is a constant negative force, so the player returns to the ground quickly when they jump
            rb.AddRelativeForce(groundMoveValue.x * playerSpeed, playerDownwardForce, groundMoveValue.y * playerSpeed);

            ///forcemode documentation below
            /// https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Rigidbody.AddForce.html
            /// There are 4 different types of forces you can apply, what each does is detailed in 
            /// the documentation
            ///for 3rdPersonHardlock cam
            rb.AddRelativeTorque(0,turnMoveValue.x * playerTurnSpeed,0, ForceMode.VelocityChange);
        }
        else
        {
            ///for freelook cam, we want to add force, not relative force, 
            /// because the player will always be moving in relation to worldspace, 
            /// not in relation to the direction they're facing
            rb.AddForce(groundMoveValue.x * playerSpeed, playerDownwardForce, groundMoveValue.y * playerSpeed);

            Vector3 movementDirection = new Vector3(groundMoveValue.x, 0, groundMoveValue.y);
            transform.forward = movementDirection;




        }


        //Jump Stuff

        //first, make a raycast and check if the character is on the ground
        //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Physics.Raycast.html
        //raycast documentation above
        //We shoot the ray from the transform.position of the object the script is attached to, 
        //in the direction of Vector3.down is straight down, 
        //the ray travels 1.2 meters, which should be just below our feet
        //and if it hits something in the groundMask layer, 
        //it returns True, otherwise returns False

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.3f, groundMask);

        Debug.DrawRay(transform.position, Vector3.down, Color.red, 1.3f);


        //now we check to see if keys attached to the Jump action were
        //pressed this frame, and if isGrounded  is true. 
        // and if so, apply impulse force upward


    }



}
