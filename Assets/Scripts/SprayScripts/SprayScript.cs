using UnityEngine;
using UnityEngine.InputSystem; ///add the input system because we're using it

public class SprayScript : MonoBehaviour
{
    //vars
    //Game Objects
    [SerializeField]private GameObject playerObj; ///need playerObj to test distance
    [SerializeField]private GameObject windControlObj; //need this to check if the player has an active green lantern
    [SerializeField]private Rigidbody playerRb; //Need to get the player's rigidbody
                                                //to blow the player around



    [SerializeField]private float windCalmTimer; ///counts down how long you can calm the wind
    
    //How much force the wind jump is
    [SerializeField]private float windForceY;

    //WindJump Charges
    [SerializeField]private int windjumpCharges;    

    ///input stuff
    /// 
    //The Action map is located in Project Window>Assets, and is called InputSystem_Actions

    [SerializeField] private InputActionAsset InputActions; //put InputSystem_Actions in here
                                                            //in the Inspector

    //input actions, doubleclick InputSystem_Actions to see these
    //or create or edit these
    private InputAction useBasicToolInput; ///Activate the basic tool with left mouse button
    private InputAction useAdvancedToolInput; ///Activate the advanced tool with right mouse button

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable(); //Get the Player actionmap
        //If the bucket is enabled, enable the inputs
        useBasicToolInput = InputSystem.actions.FindAction("BasicTool");//connect the input variable
        useAdvancedToolInput = InputSystem.actions.FindAction("AdvTool");//with the action name
        windjumpCharges = 3;
    }

    private void OnDisable()
    {

    }



    // Update is called once per frame
    void Update()
    {
        ///if they press LMB and the green lantern isn't out
        if((useBasicToolInput.WasPressedThisFrame()) && (windControlObj.activeInHierarchy == false))
        {
            windCalmTimer = 10;
            windControlObj.SetActive(true); 

        }

        ///if they press RMB and the green lantern isn't out
        if((useAdvancedToolInput.WasPressedThisFrame()) && (windjumpCharges > 0))
        {
            windjumpCharges --;//subtract one each time used
            playerRb.AddForce(0, windForceY, 0, ForceMode.Impulse); ///Apply upward force

        }   

        WindCalmTimer(); ///timer tracks how long you can calm the winds for

    }

    void WindCalmTimer()
    {
        if(windCalmTimer >0) ///If there's still time on the candle
        {
            windControlObj.SetActive(true); 
            windCalmTimer -= Time.deltaTime;
        }
        else
        {
            windControlObj.SetActive(false); 
        }
    }

}
