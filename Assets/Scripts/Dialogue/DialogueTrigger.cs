using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool isPlayerInRange;

    private void Awake()
    {
        isPlayerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && !DialogueManager.GetInstance().DialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                if (inkJSON != null)
                {
                    DialogueManager.GetInstance().EnterDialogue(inkJSON);
                }
                else
                {
                    Debug.LogWarning("Ink JSON is not assigned in the DialogueTrigger component.");
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            visualCue.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            visualCue.SetActive(false);
            isPlayerInRange = false;
        }
    }
}
