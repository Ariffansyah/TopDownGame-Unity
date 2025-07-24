using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMPro.TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choiceTexts;

    private Story currentStory;

    public bool DialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of DialogueManager detected. Destroying the new instance.");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
        DialogueIsPlaying = false;

        choiceTexts = new TextMeshProUGUI[choices.Length];
        int i = 0;
        foreach (GameObject choice in choices)
        {
            choiceTexts[i] = choice.GetComponentInChildren<TextMeshProUGUI>();
            i++;
        }
    }

    public void Update()
    {
        if (DialogueIsPlaying)
        {
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                DisplayNextLine();
            }
        }
    }

    public void EnterDialogue(TextAsset InkJSON)
    {
        currentStory = new Story(InkJSON.text);
        dialoguePanel.SetActive(true);
        DialogueIsPlaying = true;
        DisplayNextLine();
    }

    public IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(0.2f);
        dialoguePanel.SetActive(false);
        DialogueIsPlaying = false;
        currentStory = null;
    }

    private void DisplayNextLine()
    {
        if (currentStory.canContinue)
        {
            string line = currentStory.Continue();
            DisplayChoices();
            dialogueText.text = line;
        }
        else
        {
            StartCoroutine(ExitDialogue());
            dialogueText.text = string.Empty;
        }
    }

    public void DisplayChoices()
    {
        List<Choice> choicesList = currentStory.currentChoices;
        if (choicesList.Count > choices.Length)
        {
            Debug.LogWarning("Not enough choice UI elements to display all choices.");
        }

        int index = 0;
        foreach (Choice choice in choicesList)
        {
            choices[index].SetActive(true);
            choiceTexts[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0]);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
