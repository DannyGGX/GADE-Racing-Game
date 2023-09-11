using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image currentSpeakerAvatar;
    [SerializeField] private TextMeshProUGUI currentSpeakerName;
    [SerializeField] private TextMeshProUGUI currentSpeakerText;
    [Space]
    [SerializeField] private ConversationSO conversationSO;
    //[Tooltip("Measured in letter per second")]
    //[SerializeField] private float textTypeSpeed = 20;
    [Space]
    [SerializeField] private EventSenderSO onDialogueFinished;

    private Queue<DialogueEntry> dialogueEntries = new Queue<DialogueEntry>();
    private bool currentTextFinishedTyping = true;

    private void Awake()
    {
        CacheDialogue();
        NextEntry();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(dialogueEntries.Count > 0)
            {
                if(currentTextFinishedTyping)
                {
                    NextEntry();
                }
                else
                {
                    PrintWholeDialogueEntryText();
                }    
            }
            else
            {
                onDialogueFinished.Invoke(); // To GameManager to change scene
                this.Log("Dialogue finished".Color("green"));
            }
        }
    }
    private void CacheDialogue()
    {
        foreach(DialogueEntry entry in conversationSO.Conversation)
        {
            dialogueEntries.Enqueue(entry);
        }
    }

    private void NextEntry()
    {
        currentSpeakerAvatar.sprite = dialogueEntries.Peek().Speaker.Avatar;
        currentSpeakerName.text = dialogueEntries.Peek().Speaker.Name;
        StartCoroutine(nameof(TypeText), dialogueEntries.Peek().Text);
    }

    private void PrintWholeDialogueEntryText()
    {
        StopCoroutine(nameof(TypeText));
        currentSpeakerText.text = dialogueEntries.Peek().Text;
        currentTextFinishedTyping = true;
        dialogueEntries.Dequeue();
    }

    private IEnumerator TypeText(string text)
    {
        currentTextFinishedTyping = false;
        char[] chars = text.ToCharArray();
        currentSpeakerText.text = "";

        for (int i = 0; i < chars.Length; i++)
        {
            currentSpeakerText.text += chars[i];
            yield return new WaitForSeconds(ToSecondWaitTime(dialogueEntries.Peek().TextTypeSpeed));
        }
        currentTextFinishedTyping = true;
        dialogueEntries.Dequeue();
    }

    private float ToSecondWaitTime(float letterPerSecondTime)
    {
        return 1 / letterPerSecondTime;
    }
}
