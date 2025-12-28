using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.UI;
using PlayerMovement = PlayerStateMachine;

public class DialogueTriggerAdvanced : MonoBehaviour
{
    public TextMeshProUGUI dialogueTextElement;
    public GameObject textBox;
    public GameObject Test_Puzzle;

    public GameObject btnPuzzle, btnExploration;
    public GameObject puzzleOnlyActivated;

    [TextArea(2, 4)]
    public List<string> dialogueText = new List<string>();

    private int currentDialogueIndex;
    private bool isDialogueActive;
    private bool isTyping;
    private Coroutine typingCoroutine;

    private PlayerMovement playerMovementScript;
    public PlayableDirector timeline_1;
    public CawanController cawan;

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDialogueActive)
        {
            // <-- MODIFIKASI: Dapatkan script movement dari player yang masuk
            playerMovementScript = collision.GetComponent<PlayerMovement>();

            if(cawan != null)
            {
                //cawan.LightWithTrigger();
            }

            if(timeline_1 != null)
            {
                timeline_1.Play();
                playerMovementScript.playerStats.move = 0;
            }
        }
    }

    public void StartDialogueExternally(PlayerMovement playerScript)
    {
        if (isDialogueActive) return;

        if (playerScript == null)
        {
            Debug.LogError("StartDialogueExternally dipanggil tapi playerScript null!");
            return;
        }

        Debug.Log("Dialogue dimulai secara eksternal oleh script lain!");

        playerMovementScript = playerScript;

        StartFullDialogue();
    }

    public void StartFullDialogue()
    {
        isDialogueActive = true;
        textBox.gameObject.SetActive(true);
        currentDialogueIndex = 0;

        // <-- TAMBAHAN: Matikan pergerakan player
        if (playerMovementScript != null)
        {
            playerMovementScript.canMove = false;
        }

        DisplayLine();
    }

    void AdvanceDialogue()
    {
        if (isTyping)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            dialogueTextElement.text = dialogueText[currentDialogueIndex];
            isTyping = false;
        }
        else
        {
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogueText.Count)
            {
                DisplayLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void DisplayLine()
    {
        typingCoroutine = StartCoroutine(TypeLine(dialogueText[currentDialogueIndex]));
    }

    IEnumerator TypeLine(string text)
    {
        isTyping = true;
        dialogueTextElement.text = string.Empty;
        foreach (char c in text.ToCharArray())
        {
            dialogueTextElement.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        textBox.gameObject.SetActive(false);
        dialogueTextElement.text = string.Empty;
        currentDialogueIndex = 0;

        if(btnPuzzle != null && btnExploration != null)
        {
            btnPuzzle.gameObject.SetActive(true);
            btnExploration.gameObject.SetActive(true);
        }

        if(Test_Puzzle != null)
        {
            Test_Puzzle.SetActive(true);
        }

        playerMovementScript = null;
    }

    public void ContinueExplore()
    {
        playerMovementScript.EnableControl();

        btnPuzzle.gameObject.SetActive(false);
        btnExploration.gameObject.SetActive(false);
    }

    public void ActivatePuzzleOnly()
    {
        puzzleOnlyActivated.gameObject.SetActive(true);

        btnPuzzle.gameObject.SetActive(false);
        btnExploration.gameObject.SetActive(false);
    }
}