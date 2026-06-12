using UnityEngine;
using UnityEngine.InputSystem; 

public class AnnConv1 : MonoBehaviour
{

    ///vars///

    /// int that keeps track of which line of dialogue we're having
    [SerializeField] private int whichLine;

    ///GameObjects we will turn on and off
    [SerializeField] private GameObject annStart;

    //Input Actions
    private InputAction dialogueOption1;


    private void Awake()
    {
        //point toggleDialogue to the correct action in the actionmap
        dialogueOption1 = InputSystem.actions.FindAction("DialogueOption1");

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
            case 0:
            annStart.SetActive(true);///turn the correct dialogue on            
            
            if(dialogueOption1.WasPressedThisFrame())
            {
                whichLine = 0; //set the line back to the beginning
                DialogueEngine.whichConversation = 0; ///get the class, then the public variable
            }
            break;
        }
    }


}
