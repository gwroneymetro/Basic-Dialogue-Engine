using UnityEngine;
using UnityEngine.InputSystem; ///add the input system because we're using it

public class LanternScript : MonoBehaviour
{
    //vars
    //Game Objects
    [SerializeField]private GameObject greenLightObj; //The green light that hides
    [SerializeField]private GameObject redLightObj;   //the red light that reveals

    ///Lantern timers
    [SerializeField]private float greenLightTimer; 
    [SerializeField]private float redLightTimer; 

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
        if((useBasicToolInput.WasPressedThisFrame()) && (greenLightObj.activeInHierarchy == false))
        {
            greenLightTimer = 10;
        }   

        ///if they press RMB and the green lantern isn't out
        if((useAdvancedToolInput.WasPressedThisFrame()) && (redLightObj.activeInHierarchy == false))
        {
            redLightTimer = 10;
        }   

        ///Right now I have it so they can have both lanterns out at the same time, 
        /// and we might need to change that, 
        /// but that's easy enough to do by changing the above if statements

        GreenLanternCandleBurns(); ///timer tracks whether the green 
        RedLanternCandleBurns(); ///and red lights are active

    }

    void GreenLanternCandleBurns()
    {
        if(greenLightTimer >0) ///If there's still time on the candle
        {
            greenLightObj.SetActive(true); 
            greenLightTimer -= Time.deltaTime;
        }
        else
        {
            greenLightObj.SetActive(false); 
        }
    }

    void RedLanternCandleBurns()
    {
        if(redLightTimer >0) ///If there's still time on the candle
        {
            redLightObj.SetActive(true); 
            redLightTimer -= Time.deltaTime;
        }
        else
        {
            redLightObj.SetActive(false); 
        }
    }




}
