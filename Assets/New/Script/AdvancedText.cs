using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class AdvancedText : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textElement;
    private GameObject dialogueBox;
    private GameObject block;

    [Header("Settings")]
    public float charDelay = 0.05f; // Dipercepat default-nya biar enak
    public bool allowSkip = true;

    [Header("Auto Play (Untuk Intro)")]
    public bool playOnStart = false;
    [TextArea(2, 5)]
    public List<string> linesToPlayOnStart;

    [Header("Auto Advance")]
    public bool autoAdvance = true;
    public float lineDelay = 0.7f;

    [Header("Events")]
    // Event ini muncul di Inspector, bisa ditarik-tarik
    public UnityEvent OnDialogueComplete;

    // Variable internal
    private List<string> dialogues = new List<string>();
    private int index = -1;
    private bool isTyping = false;
    private bool finishedAll = false;

    public bool isHint;

    // Action untuk script lain (Grid/Controller) yang ingin 'menunggu' via code
    public System.Action OnAllDialogueFinishedAction;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        dialogueBox = GameObject.FindWithTag("DialogueBox");
        block = GameObject.FindWithTag("UIBlock");
    }

    void Start()
    {
        // Fitur Auto Start (Untuk Intro)
        if (playOnStart && linesToPlayOnStart.Count > 0)
        {
            dialogueBox.GetComponent<Image>().enabled = true;

            if (isHint)
            {
                block.GetComponent<Image>().enabled = true;
            }
            
            StartDialogue(linesToPlayOnStart);
        }

        //Debug.Log(index + " : " + dialogues.Count);
    }

    public void StartDialogue(List<string> lines)
    {
        if (lines == null || lines.Count == 0) return;

        StopAllCoroutines();
        dialogues = lines;
        index = -1;
        finishedAll = false;

        NextLine();
    }

    public void ForceAdvance()
    {
        if (finishedAll) return;

        if (isTyping)
        {
            InstantFinishCurrentLine();
        }
        else
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        index++;

        if (index >= dialogues.Count)
        {
            AllFinished();
            return;
        }

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine(dialogues[index]));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        textElement.text = "";

        foreach (char c in line)
        {
            textElement.text += c;
            yield return new WaitForSeconds(charDelay);
        }

        isTyping = false;

        // AUTO LANJUT
        if (autoAdvance)
        {
            yield return new WaitForSeconds(lineDelay);
            NextLine();
        }
    }

    private void InstantFinishCurrentLine()
    {
        if (index < 0 || index >= dialogues.Count) return;

        isTyping = false;
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        textElement.text = dialogues[index];
    }

    private void AllFinished()
    {
        finishedAll = true;
        Debug.Log("Finished");
        textElement.text = string.Empty;

        dialogueBox.GetComponent<Image>().enabled = false;
        block.GetComponent<Image>().enabled = false;

        if (OnDialogueComplete != null) OnDialogueComplete.Invoke();

        if (OnAllDialogueFinishedAction != null) OnAllDialogueFinishedAction.Invoke();
    }

    public void AddIndex()
    {
        index++;
    }
}