using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WhispDialogueTeleportPuzzle : MonoBehaviour
{
    [SerializeField] private List<string> dialogue = new List<string>();
    [SerializeField] private TextMeshProUGUI dialogueTextElement;
    [SerializeField] private Image BoxDialogue;

    private bool cooldown;
    private bool finishConversation;
    public float typingSpeed;
    public float delayAfterType;

    private void Update()
    {
        if (finishConversation)
        {
            dialogueTextElement.text = string.Empty;
            BoxDialogue.gameObject.SetActive(false);
        }
    }

    public void ConversationAfterWhisper()
    {
        finishConversation = false;
        StartCoroutine(Conversation());
    }

    IEnumerator Conversation()
    {
        for (int i = 0; i < dialogue.Count; i++)
        {
            TypeLine(dialogue[i]);
            yield return new WaitForSeconds(5f);

            if (i == dialogue.Count - 1) 
                finishConversation = true;
        }
    }

    IEnumerator TypeLine(string text)
    {
        cooldown = true;
        dialogueTextElement.text = string.Empty;

        foreach (char c in text.ToCharArray()) // .ToCharArray() lebih aman
        {
            dialogueTextElement.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(delayAfterType);
        cooldown = false;
    }
}
