using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [Header("Containers")]
    public Transform slotContainer;      // Parent dari semua Slot (jawaban)
    public Transform solutionContainer;  // Parent dari solusi
    public Transform piecesContainer;    // Parent dari semua Piece

    [Header("UI")]
    public Button submitButton;
    public Button resetButton;
    public TextMeshProUGUI attemps;
    public TextMeshProUGUI time;

    [Header("Colors / Visuals")]
    public Sprite emptySlotSprite;       // Gambar slot kosong (opsional)
    public int attemptCount = 0;

    public List<Slot> allSlots = new List<Slot>();        // Grid jawaban
    public List<Slot> solutionSlots = new List<Slot>();   // Grid solusi
    private List<Piece> allPieces = new List<Piece>();

    private float secondClock, minuteClock;
    private bool isComplete, isStart;
    
    void Start()
    {
        AssignSlotsFromChildren();
        AssignSolutionFromChildren();

        foreach (Transform child in piecesContainer)
        {
            Piece piece = child.GetComponent<Piece>();
            if (piece != null)
            {
                piece.manager = this;
                allPieces.Add(piece);
            }
        }

        if (submitButton != null)
            submitButton.onClick.AddListener(CheckSolution);

        if (resetButton != null)
            resetButton.onClick.AddListener(ResetAll);
    }

    private void Update()
    {
        attemps.text = "Attemps : " + attemptCount;

        if (!isComplete && isStart)
        {
            secondClock += Time.deltaTime;
        }

        if (secondClock >= 60)
        {
            minuteClock++;
            secondClock = 0;
        }

        time.text = "Time   : " + minuteClock + $" : {secondClock:F0}";
    }

    public void StartingPuzzle()
    {
        isStart = true;
    }

    public void AssignSlotsFromChildren()
    {
        allSlots.Clear();
        foreach (Transform child in slotContainer)
        {
            Slot s = child.GetComponent<Slot>();
            if (s != null)
            {
                s.Clear();
                allSlots.Add(s);
            }
        }
    }

    public void AssignSolutionFromChildren()
    {
        solutionSlots.Clear();
        foreach (Transform child in solutionContainer)
        {
            Slot s = child.GetComponent<Slot>();
            if (s != null)
            {
                solutionSlots.Add(s);
            }
        }
    }

    public void CheckSolution()
    {
        if (allSlots.Count != solutionSlots.Count)
        {
            Debug.LogWarning("Grid size mismatch!");
            return;
        }

        bool correct = true;

        for (int i = 0; i < allSlots.Count; i++)
        {
            //Debug
            //Debug.Log($"Slot {i} - Answer: {allSlots[i].state}, Solution: {solutionSlots[i].state}");

            Slot answer = allSlots[i];
            Slot target = solutionSlots[i];

            if (answer.state != target.state)
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            Debug.Log("Jawaban benar!");
            isStart = false;
        }
        else
        {
            Debug.Log("Jawaban salah!");
            attemptCount++;
            ResetAll();
        }
    }

    public void ResetSlots()
    {
        foreach (Slot s in allSlots)
            s.Clear();
    }

    public void ResetPieces()
    {
        foreach (Piece p in allPieces)
            p.ResetToStart();
    }

    public void ResetAll()
    {
        ResetSlots();
        ResetPieces();
    }

}
