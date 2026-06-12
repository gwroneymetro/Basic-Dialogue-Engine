using UnityEngine;
using UnityEngine.InputSystem; 

public class JohnConv1 : MonoBehaviour
{
    ///vars///

    /// int that keeps track of which line of dialogue we're having
    [SerializeField] private int whichLine;

    ///GameObjects we will turn on and off
    [SerializeField] private GameObject johnStart;
    [SerializeField] private GameObject john10;
    [SerializeField] private GameObject john11;
    [SerializeField] private GameObject john12;
    [SerializeField] private GameObject john20;
    [SerializeField] private GameObject john21;
    [SerializeField] private GameObject john22;    


    //Input Actions
    private InputAction dialogueOption1;
    private InputAction dialogueOption2;

    private void Awake()
    {
        //point toggleDialogue to the correct action in the actionmap
        dialogueOption1 = InputSystem.actions.FindAction("DialogueOption1");
        dialogueOption2 = InputSystem.actions.FindAction("DialogueOption2");   
    }

    // Update is called once per frame
    void Update()
    {
        LetsTalk();
    }

    void LetsTalk()//The function that switches between different lines
    {
        switch(whichLine)
        {
        case 0: ///conversation starts, turn other lines off
        johnStart.SetActive(true);///turn the correct dialogue on
        john10.SetActive(false);///and the rest off
        john11.SetActive(false);
        john12.SetActive(false);
        john20.SetActive(false);
        john21.SetActive(false);
        john22.SetActive(false);

        ///depending on input, switch which dialogue is active
        if(dialogueOption1.WasPressedThisFrame())
        {
            whichLine = 10;
        }

        if(dialogueOption2.WasPressedThisFrame())
        {
            whichLine = 20;
        }
        break;

        case 10: ///conversation starts, turn other lines off
        johnStart.SetActive(false);
        john10.SetActive(true);
        john11.SetActive(false);
        john12.SetActive(false);
        john20.SetActive(false);
        john21.SetActive(false);
        john22.SetActive(false);

        ///depending on input, switch which dialogue is active
        if(dialogueOption1.WasPressedThisFrame())
        {
            whichLine = 11;
        }

        if(dialogueOption2.WasPressedThisFrame())
        {
            whichLine = 12;
        }
        break;        


        case 11: ///conversation starts, turn other lines off
        johnStart.SetActive(false);
        john10.SetActive(false);
        john11.SetActive(true);
        john12.SetActive(false);
        john20.SetActive(false);
        john21.SetActive(false);
        john22.SetActive(false);
        //since this is the end of the dialogue tree,
        //reset whichLine to the beginning of the conversation and 
        // turn off the dialogue engine
        //by setting whichConversation to 0.
        if(dialogueOption1.WasPressedThisFrame())
        {
            whichLine = 0; //set the line back to the beginning
            DialogueEngine.whichConversation = 0; ///get the class, then the public variable

        }


        break;

        case 12: ///conversation starts, turn other lines off
        johnStart.SetActive(false);
        john10.SetActive(false);
        john11.SetActive(false);
        john12.SetActive(true);
        john20.SetActive(false);
        john21.SetActive(false);
        john22.SetActive(false);
        //since this is the end of the dialogue tree,
        //reset whichLine to the beginning of the conversation and 
        // turn off the dialogue engine
        //by setting whichConversation to 0.
        if(dialogueOption1.WasPressedThisFrame())
        {
            whichLine = 0; //set the line back to the beginning
            DialogueEngine.whichConversation = 0; ///get the class, then the public variable

        }
        break;

        case 20: ///conversation starts, turn other lines off
        johnStart.SetActive(false);
        john10.SetActive(false);
        john11.SetActive(false);
        john12.SetActive(false);
        john20.SetActive(true);
        john21.SetActive(false);
        john22.SetActive(false);
        ///depending on input, switch which dialogue is active
        if(dialogueOption1.WasPressedThisFrame())
        {
            whichLine = 21;
        }

        if(dialogueOption2.WasPressedThisFrame())
        {
            whichLine = 22;
        }
        break;

        case 21: ///conversation starts, turn other lines off
        johnStart.SetActive(false);
        john10.SetActive(false);
        john11.SetActive(false);
        john12.SetActive(false);
        john20.SetActive(false);
        john21.SetActive(true);
        john22.SetActive(false);
        //since this is the end of the dialogue tree,
        //reset whichLine to the beginning of the conversation and 
        // turn off the dialogue engine
        //by setting whichConversation to 0.
        if(dialogueOption1.WasPressedThisFrame())
        {
            whichLine = 0; //set the line back to the beginning
            DialogueEngine.whichConversation = 0; ///get the class, then the public variable

        }
        break;

        case 22: ///conversation starts, turn other lines off
        johnStart.SetActive(false);
        john10.SetActive(false);
        john11.SetActive(false);
        john12.SetActive(false);
        john20.SetActive(false);
        john21.SetActive(false);
        john22.SetActive(true);
        //since this is the end of the dialogue tree,
        //reset whichLine to the beginning of the conversation and 
        // turn off the dialogue engine
        //by setting whichConversation to 0.
        if(dialogueOption1.WasPressedThisFrame())
        {
            whichLine = 0; //set the line back to the beginning
            DialogueEngine.whichConversation = 0; ///get the class, then the public variable

        }
        break;
        }
    }
}
