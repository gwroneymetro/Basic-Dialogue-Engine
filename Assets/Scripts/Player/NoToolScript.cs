using UnityEngine;
using UnityEngine.InputSystem; ///add the input system because we're using it

public class NoToolScript : MonoBehaviour
{
    //vars
    //Game Objects
    [SerializeField]private GameObject glideObj; //need this to check if the player has an active green lantern
    [SerializeField]private GameObject dashObj; //need this to check if the player has an active green lantern




    [SerializeField]private float dashTimer; //how long you dash
    [SerializeField]private float glideTimer;   ///how long you glide


    //WindJump Charges
    [SerializeField]private int  staminaCharges;    

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

    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ///if they press LMB and the green lantern isn't out
        if((useBasicToolInput.WasPressedThisFrame()) && (dashObj.activeInHierarchy == false))
        {
            dashTimer = .1f;


        }

        ///if they press RMB and the green lantern isn't out
        if((useAdvancedToolInput.WasPressedThisFrame()) && (glideObj.activeInHierarchy == false))
        {
            glideTimer = 3;
        }   

        DashTimer(); ///timer tracks how long you can calm the winds for
        GlideTimer(); ///timer tracks how long you can calm the winds for
    }

    void DashTimer()
    {
        if(dashTimer >0) ///If there's still time on the candle
        {
            dashObj.SetActive(true); 
            dashTimer -= Time.deltaTime;
        }
        else
        {
            dashObj.SetActive(false); 
        }
    }

    void GlideTimer()
    {
        if(glideTimer >0) ///If there's still time on the candle
        {
            glideObj.SetActive(true); 
            glideTimer -= Time.deltaTime;
        }
        else
        {
            glideObj.SetActive(false); 
        }
    }

}
