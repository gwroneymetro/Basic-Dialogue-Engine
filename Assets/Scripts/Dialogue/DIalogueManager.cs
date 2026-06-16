using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private Story currentStory;

    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private Transform choicesPanel;

    [SerializeField] private GameObject choiceButtonPrefab;

    [SerializeField] private GameObject dialogueCanvas;
    
    
    public bool DialogueActive
    {
        get { return currentStory != null; }
    }

    private void Awake()
    {
        Instance = this;
        dialogueCanvas.SetActive(false);

    }

    public void StartStory(TextAsset inkJSON)
    {
        dialogueCanvas.SetActive(true);
        currentStory = new Story(inkJSON.text);

        ContinueStory();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ContinueStory()
    {
        ClearChoices();

        if (currentStory == null)
            return;

        if (currentStory.canContinue)
        {
            string text = currentStory.Continue();
            dialogueText.text = text;
            Debug.Log(text);
        }

        else if (currentStory.currentChoices.Count > 0)
        {
            dialogueText.text = "";

            DisplayChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueText.text = "";
        Debug.Log("Story Ended");
        currentStory = null;
        dialogueCanvas.SetActive(false);    
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ChooseChoice(int index)
    {

        ClearChoices();
        currentStory.ChooseChoiceIndex(index);
        ContinueStory();
    }

    private void DisplayChoices()
    {
        // Clear old buttons
        foreach (Transform child in choicesPanel)
        {
            Destroy(child.gameObject);
        }

        // Create new buttons
        for (int i = 0; i < currentStory.currentChoices.Count; i++)
        {
            Choice choice = currentStory.currentChoices[i];

            GameObject buttonObj =
                Instantiate(choiceButtonPrefab, choicesPanel);
            
            Debug.Log("Created button for choice: " + choice.text + "and button is: " + buttonObj);

            TMP_Text buttonText =
                buttonObj.GetComponentInChildren<TMP_Text>();

            buttonText.text = choice.text;

            int choiceIndex = i;

            Button button = buttonObj.GetComponent<Button>();

            button.onClick.AddListener(() =>
            {
                ChooseChoice(choiceIndex);
            });
        }
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(choicesPanel as RectTransform);
    }

    private void ClearChoices()
    {
        foreach (Transform child in choicesPanel)
        {
            Destroy(child.gameObject);
        }
    }
}