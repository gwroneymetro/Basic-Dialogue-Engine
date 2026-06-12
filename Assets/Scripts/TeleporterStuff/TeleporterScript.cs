using UnityEngine;
using UnityEngine.InputSystem; ///add the input system because we're using it

public class TeleporterScript : MonoBehaviour
{
    ///vars
    


    ///teleporter prefab gameobjects
    [SerializeField] private GameObject lmbInPuddle;
    [SerializeField] private GameObject lmbOutPuddle;
    [SerializeField] private GameObject rmbFirstPortal;
    [SerializeField] private GameObject rmbSecondPortal;   

    private GameObject lmbPuddle1;   
    private GameObject lmbPuddle2;   
    private GameObject rmbPuddle1;   
    private GameObject rmbPuddle2;   

    ///variables that keep track of the number of teleport puddles placed
    private int lmbPuddlesPlaced;
    private int rmbPuddlesPlaced;

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
        if(useBasicToolInput.WasPressedThisFrame()) //if LMB pressed, 
        {
            ///Switch checks how many puddles have been placed 
            /// and does the appropriate thing

            switch(lmbPuddlesPlaced) 
            {
                case 0: ///0 puddles? Place the In puddle
                //Create the appropriate puddle
                //the prefab you dragged into the lmbInPuddle variable
                //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Object.Instantiate.html
                Instantiate(lmbInPuddle, transform.position, transform.rotation);
                lmbPuddlesPlaced++; //add 1 to the number of puddles placed
                break;

                case 1: ///1 puddle? Place the Out puddle
                //Create the appropriate puddle
                //the prefab you dragged into the lmbInPuddle variable
                //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Object.Instantiate.html
                Instantiate(lmbOutPuddle, transform.position, transform.rotation);
                lmbPuddlesPlaced++; //add 1 to the number of puddles placed
                break;

                case 2: ///2 puddles already? Find the two existing puddles, 
                //destroy them, and place the new In puddle

                ///Find the two existing puddles
                /// https://docs.unity3d.com/6000.4/Documentation/ScriptReference/GameObject.Find.html
                lmbPuddle1 = GameObject.Find("PuddleLMBIn(Clone)");
                lmbPuddle2 = GameObject.Find("PuddleLMBOut(Clone)");
                /// Destroy those existing puddles
                /// https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Object.Destroy.html
                Destroy(lmbPuddle1);
                Destroy(lmbPuddle2);

                //Create the appropriate puddle
                //the prefab you dragged into the lmbInPuddle variable
                //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Object.Instantiate.html
                Instantiate(lmbInPuddle, transform.position, transform.rotation);
                lmbPuddlesPlaced = 1; //set the number of existing puddles to 1 
                break;
            }
        }

        ///then do basically the exact same thing for the Advanced tool
        if(useAdvancedToolInput.WasPressedThisFrame()) //if LMB pressed, 
        {
            ///Switch checks how many puddles have been placed 
            /// and does the appropriate thing

            switch(rmbPuddlesPlaced) 
            {
                case 0: ///0 puddles? Place the In puddle
                //Create the appropriate puddle
                //the prefab you dragged into the lmbInPuddle variable
                //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Object.Instantiate.html
                Instantiate(rmbFirstPortal, transform.position, transform.rotation);
                rmbPuddlesPlaced++; //add 1 to the number of puddles placed
                break;

                case 1: ///1 puddle? Place the Out puddle
                //Create the appropriate puddle
                //the prefab you dragged into the lmbInPuddle variable
                //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Object.Instantiate.html
                Instantiate(rmbSecondPortal, transform.position, transform.rotation);
                rmbPuddlesPlaced++; //add 1 to the number of puddles placed
                break;

                case 2: ///2 puddles already? Find the two existing puddles, 
                //destroy them, and place the new In puddle

                ///Find the two existing puddles
                /// https://docs.unity3d.com/6000.4/Documentation/ScriptReference/GameObject.Find.html
                rmbPuddle1 = GameObject.Find("PuddleRMB1(Clone)");
                rmbPuddle2 = GameObject.Find("PuddleRMB2(Clone)");
                /// Destroy those existing puddles
                /// https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Object.Destroy.html
                Destroy(rmbPuddle1);
                Destroy(rmbPuddle2);

                //Create the appropriate puddle
                //the prefab you dragged into the lmbInPuddle variable
                //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Object.Instantiate.html
                Instantiate(rmbFirstPortal, transform.position, transform.rotation);
                rmbPuddlesPlaced = 1; //set the number of existing puddles to 1 
                break;
            }
        }
    }


}
