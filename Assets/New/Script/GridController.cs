using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridController : MonoBehaviour
{
    [Header("Grid Compare")]
    public List<GridBox> _mainGrid = new List<GridBox>();
    public List<GridBox> _answerGrid = new List<GridBox>();

    [Header("Puzzle Data")]
    [TextArea(3, 5)]
    public string narrativeHint;

    [Header("Data")]
    public int wrongSubmitCount = 0;
    public int resetCount = 0;
    public float timeElapsed = 0;

    public UnityEvent OnPuzzleFinished;
    private PuzzleController myManager;

    void Start()
    {
        myManager = FindObjectOfType<PuzzleController>();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    public bool CheckAnswer()
    {
        if (_mainGrid.Count != _answerGrid.Count)
        {
            Debug.Log("Jumlah Grid Tidak sama");
            return false;
        }

        for (int i = 0; i < _mainGrid.Count; i++)
        {
            if (_mainGrid[i].isActive != _answerGrid[i].isActive)
            {
                return false;
            }
        }
        return true;
    }
    
    public void OnSubmitButton()
    {
        if (CheckAnswer())
        {
            StartCoroutine(RunSuccessSequence());

            if (OnPuzzleFinished != null)
            {
                OnPuzzleFinished.Invoke();
            }
        }
        else
        {
            StartCoroutine(RunFailedSequence());
        }
    }

    public void ResetPuzzle()
    {
        // 1. Loop ke semua kotak di mainGrid
        foreach (GridBox box in _mainGrid) // Atau GridBox, sesuaikan nama script Anda
        {
            if (box != null)
                box.ResetGridSquare();
        }

        // 2. Tambah Counter Reset (Untuk pengurangan poin -15 nanti)
        resetCount++;

        Debug.Log("Puzzle Direset. Penalti reset bertambah.");
    }

    public int GetFinalScore()
    {
        int maxScore = 100;

        // Hitung penalti
        int penalty = (wrongSubmitCount * 5) + (resetCount * 10);

        // Hitung penalti waktu (opsional, misal -1 per 10 detik lewat dari 1.5 menit)
        if (timeElapsed > 240)
        {
            penalty += Mathf.FloorToInt((timeElapsed - 60) / 10f);
        }

        int finalScore = maxScore - penalty;

        // Balikin nilai (Minimal 0)
        return Mathf.Max(0, finalScore);
    }

    #region Ienumerator Collection


    IEnumerator RunSuccessSequence()
    {
        yield return new WaitForSeconds(1f);

        if (myManager != null)
        {
            // "Bos, saya sudah selesai. Tolong catat nilai saya dan ganti level."
            myManager.AdvanceToNextPuzzle();
        }
        else
        {
            Debug.LogError("Grid ini kehilangan referensi ke PuzzleController (myManager)!");
        }
    }

    IEnumerator RunFailedSequence()
    {
        wrongSubmitCount++;
        ResetPuzzle();
        yield return new WaitForSeconds(1f);
    }

    #endregion
}
