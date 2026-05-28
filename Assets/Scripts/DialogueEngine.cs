using UnityEngine;
using UnityEngine.InputSystem; 


public class DialogueEngine : MonoBehaviour
{

    ///vars///
    ///
    [SerializeField] private InputActionAsset InputActions; //put InputSystem_Actions in here

    ///Int that keeps track of which dialogue is active and controls 
    /// switch in DialogueSwitcher function, it's static so other scripts can change it
    public static int whichConversation; //which conversation are we having? 0=none, 1=john, 2=ann
    
    //Game Objects to turn on and off
    [SerializeField] private GameObject dialogueEngine; //Set to active to enable dialogue
    [SerializeField] GameObject conversationJohn;  //Set this to active for John conv
    [SerializeField] GameObject conversationAnn;  //Set this to active for Ann conv

    //Input Actions
    private InputAction toggleDialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void OnEnable()
    {
        //go into your InputActions asset and enable the
        //"Dialogue" action map
        InputActions.FindActionMap("Dialogue").Enable();
    }

    private void OnDisable()
    {
        //Disable the Dialogue actionmap when you're not in Dialogue
        InputActions.FindActionMap("Dialogue").Enable();        
    }

    private void Awake()
    {
        //point toggleDialogue to the correct action in the actionmap
        toggleDialogue = InputSystem.actions.FindAction("DialogueToggle");
    }

    // Update is called once per frame
    void Update()
    {
        DialogueSwitcher(); //The function that keeps track of which conversation we're on

        //this is basically the "talk to" key
        //would usually have an additional condition like "nearby Ann" or "looking
        //at John"
        //In this case, we're just going to cycle between talking to nobody,
        //talking to John, and talking to Ann

        if(toggleDialogue.WasPressedThisFrame())
        {
            switch(whichConversation)
            {
                case 0: 
                whichConversation = 1;
                break;

                case 1: 
                whichConversation = 2;
                break;

                case 2: 
                whichConversation = 0;
                break;
            }
        }

    }

    void DialogueSwitcher()//The function that keeps track of which conversation we're on
    {
        switch(whichConversation)
        {
        case 0: ///if conv zero, turn off dialogue engine
        dialogueEngine.SetActive(false);
        break;

        case 1: ///if conv 1(john), turn on dialogue engine and conversation1
        dialogueEngine.SetActive(true);        
        conversationJohn.SetActive(true); //turn on the active conversation
        conversationAnn.SetActive(false); ///turn off all other conversations
        break;

        case 2: ///if conv 2(Ann), turn on dialogue engine and conversation2
        dialogueEngine.SetActive(true);        
        conversationJohn.SetActive(false); ///turn off all other conversations
        conversationAnn.SetActive(true); //turn on the active conversation 
        break;
        }
    }
}
