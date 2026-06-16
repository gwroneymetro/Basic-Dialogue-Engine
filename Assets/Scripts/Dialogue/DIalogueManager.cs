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

    // [SerializeField] private Transform choicesPanel;

    // [SerializeField] private GameObject choiceButtonPrefab;

    
    
    public bool DialogueActive
    {
        get { return currentStory != null; }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void StartStory(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);

        ContinueStory();
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string text = currentStory.Continue();

            dialogueText.text = text;
        }
        if (currentStory.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else if (!currentStory.canContinue)
        {
            ClearChoices();

            dialogueText.text = "";
            
            Debug.Log("Story Ended");
            currentStory = null;
        }
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
    }

    private void ClearChoices()
    {
        foreach (Transform child in choicesPanel)
        {
            Destroy(child.gameObject);
        }
    }
}