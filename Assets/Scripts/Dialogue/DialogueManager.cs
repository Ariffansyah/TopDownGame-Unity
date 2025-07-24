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
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayName;
    [SerializeField] private Animator speakerImage;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choiceTexts;

    private Story currentStory;

    public bool DialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string IMAGE_TAG = "image";

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of DialogueManager detected. Destroying the new instance.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
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
        for (int i = 0; i < choices.Length; i++)
        {
            choiceTexts[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
            choices[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (!DialogueIsPlaying)
            return;

        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            DisplayNextLine();
        }
    }

    public void EnterDialogue(TextAsset InkJSON)
    {
        currentStory = new Story(InkJSON.text);
        dialoguePanel.SetActive(true);
        DialogueIsPlaying = true;
        displayName.text = "???";
        speakerImage.Play("Default");
        canContinueToNextLine = false;
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
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }

        if (currentStory.canContinue)
        {
            string line = currentStory.Continue();
            HandleTags(currentStory.currentTags);
            displayLineCoroutine = StartCoroutine(TypeWritting(line));
        }
        else
        {
            StartCoroutine(ExitDialogue());
            dialogueText.text = string.Empty;
        }
    }

    private IEnumerator TypeWritting(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        HideChoices();
        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach (char letter in line)
        {
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                    isAddingRichTextTag = false;
            }
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(0.04f);
            }
        }

        dialogueText.maxVisibleCharacters = line.Length;
        DisplayChoices();
        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
    }

    private void HandleTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            string[] splitParts = tag.Split(':');
            if (splitParts.Length != 2)
            {
                Debug.LogWarning($"Invalid tag format: {tag}");
                continue;
            }

            string tagKey = splitParts[0].Trim();
            string tagValue = splitParts[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayName.text = tagValue;
                    break;
                case IMAGE_TAG:
                    speakerImage.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning($"Unknown tag: {tagKey}");
                    break;
            }
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
        for (int i = 0; i < choices.Length; i++)
        {
            if (choices[i].activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(choices[i]);
                break;
            }
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine && choiceIndex >= 0 && choiceIndex < currentStory.currentChoices.Count)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed();
            DisplayNextLine();
        }
    }
}
