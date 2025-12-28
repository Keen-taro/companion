using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    //public Timer timerPuzzle;
    public TextMeshProUGUI dialogueTextElement; // Dialogue Box
    public GameObject GridPuzzleParent;
    public GameObject ParentChoiceButton;

    [TextArea(2, 4)]
    public List<string> dialogueHints = new List<string>();

    [TextArea(2, 4)]
    public List<string> successContinuationDialogue = new List<string>();

    public List<GridSquare> mainGrid = new List<GridSquare>();
    public List<GridSquare> solutionGrid = new List<GridSquare>();

    [Header("Chaining (Rangkaian) Event")]
    [Tooltip("Masukkan GameObject DialogueTriggerAdvance yang ingin dimulai setelah puzzle ini selesai")]
    public DialogueTriggerAdvanced nextDialogueToTrigger;

    private bool cooldown;
    private bool isCorrect;
    public bool isTest;

    private PlayerStateMachine playerMovementScript;

    public int currentStep = 0; // Melacak petunjuk nomor berapa
    public int attemp;


    private List<GridSquare> allGridSquares = new List<GridSquare>(); // Untuk mereset

    [SerializeField] private string[] wrongAnswer;
    [SerializeField] private string[] successAnswer;
    [SerializeField] private string[] testFailed;

    void Start()
    {
        playerMovementScript = FindObjectOfType<PlayerStateMachine>();
        if (playerMovementScript == null)
        {
            Debug.LogError("Grid tidak bisa menemukan PlayerStateMachine di scene!");
        }

        // Isi list allGridSquares
        foreach (Transform child in transform)
        {
            GridSquare square = child.GetComponent<GridSquare>();
            if (square != null)
            {
                allGridSquares.Add(square);
            }
        }

        // Mulai puzzle
        StartPuzzle();
    }

    public void StartPuzzle()
    {
        currentStep = 0;
        ResetAllGridSquares();

        // Tampilkan petunjuk pertama
        ShowCurrentHint();
    }

    public void NextHint()
    {
        if(currentStep + 1 < dialogueHints.Count && !cooldown)
        {
            currentStep++;
            dialogueTextElement.text = string.Empty;
            StartDialogue(dialogueHints[currentStep]);
        }
    }

    public void PreviousHint()
    {
        if (currentStep != 0 && !cooldown)
        {
            currentStep--;
            dialogueTextElement.text = string.Empty;
            StartDialogue(dialogueHints[currentStep]);
        }
    }

    // Menampilkan hint/dialogue saat ini ke UI
    public void ShowCurrentHint()
    {
        if (currentStep < dialogueHints.Count && !cooldown)
        {
            StartDialogue(dialogueHints[currentStep]);
        }
    }

    public void CheckAnswer()
    {
        if (isCorrect || cooldown) return;
        StartCoroutine(CheckButtonCooldown());
    }

    IEnumerator RunSuccessSequence()
    {
        // 1. Ambil alih kontrol
        cooldown = true;
        dialogueTextElement.text = string.Empty;

        // 2. Tampilkan pesan "Sukses" acak
        string randomSuccessMsg = successAnswer[Random.Range(0, successAnswer.Length)];
        yield return StartCoroutine(StartEndDialogue(randomSuccessMsg));

        // 3. Tampilkan dialog lanjutan (jika ada)
        if (successContinuationDialogue.Count > 0)
        {
            for (int i = 0; i < successContinuationDialogue.Count; i++)
            {
                yield return StartCoroutine(StartEndDialogue(successContinuationDialogue[i]));
            }
        }

        // 4. Beri jeda 1 detik
        yield return new WaitForSeconds(1f);

        // 5. Tutup puzzle INI
        ClosePuzzle();

        if (nextDialogueToTrigger != null)
        {
            nextDialogueToTrigger.StartDialogueExternally(playerMovementScript);
        }
        else
        {
            if (playerMovementScript != null)
                playerMovementScript.canMove = true;
        }

        // 7. Lepaskan kontrol
        cooldown = false;
    }

    IEnumerator CheckButtonCooldown()
    {
        CheckGridWithTheSolution();

        while (cooldown)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (!isCorrect)
        {
            yield return new WaitForSeconds(2f);

            dialogueTextElement.text = string.Empty;
            dialogueTextElement.text = dialogueHints[currentStep];
        }
    }

    public void CheckGridWithTheSolution()
    {
        // 1. Validasi dasar
        if (mainGrid == null || solutionGrid == null)
        {
            Debug.LogError("Grid references belum di-set!");
            return;
        }

        if (mainGrid.Count != solutionGrid.Count)
        {
            Debug.LogError("Jumlah grid tidak sama!");
            return;
        }

        isCorrect = true;

        // 2. Pengecekan utama

        for (int i = 0; i < mainGrid.Count; i++)
        {
            if (mainGrid[i].isActivate != solutionGrid[i].isActivate)
            {
                isCorrect = false;
            }
        }

        // 3. Hasil pengecekan

        if (isCorrect)
        {
            StartCoroutine(RunSuccessSequence());
        }
        else if (attemp == 3 && isTest)
        {
            StartCoroutine(RunTestFailedSequence());
        }
        else
        {
            dialogueTextElement.text = string.Empty;
            StartDialogue(wrongAnswer[Random.Range(0, wrongAnswer.Length)]);
            attemp++;
        }
    }

    // Fungsi untuk mereset semua grid
    public void ResetAllGridSquares()
    {
        if (isCorrect) return;

        foreach (GridSquare square in allGridSquares)
        {
            square.ResetGridSquare();
        }
        currentStep = 0; // Kembali ke langkah 0
        // Mungkin tampilkan hint pertama lagi
        // ShowCurrentHint(); 
    }

    void StartDialogue(string text)
    {
        StartCoroutine(TypeLine(text));
    }

    IEnumerator StartEndDialogue(string text)
    {
        yield return StartCoroutine(TypeLine(text));

        yield return new WaitForSeconds(1f);
    }

    // TAMBAHKAN FUNGSI BARU INI
    IEnumerator RunTestFailedSequence()
    {
        cooldown = true;
        dialogueTextElement.text = string.Empty;

        // Loop melalui setiap baris dialog kegagalan
        for (int i = 0; i < testFailed.Length; i++)
        {
            yield return StartCoroutine(StartEndDialogue(testFailed[i]));
        }

        yield return new WaitForSeconds(1f); 
        ClosePuzzle();

        if (nextDialogueToTrigger != null)
        {
            nextDialogueToTrigger.StartDialogueExternally(playerMovementScript);
        }
        else
        {
            if (playerMovementScript != null)
                playerMovementScript.canMove = true;
        }

        // 7. Lepaskan kontrol
        cooldown = false;
    }

    private void ClosePuzzle()
    {
        GridPuzzleParent.gameObject.SetActive(false);
    }

    IEnumerator TypeLine(string text)
    {
        cooldown = true;
        dialogueTextElement.text = string.Empty; // <-- Pindahkan ini ke atas!

        foreach (char c in text.ToCharArray())
        {
            dialogueTextElement.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);
        cooldown = false;
    }
}