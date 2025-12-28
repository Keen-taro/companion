using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public class PuzzleController : MonoBehaviour
{
    public GameObject globalSubmitButton;
    private List<int> levelScores = new List<int>();
    public ResultPanelDisplay resultPanel;

    [Header("Levels")]
    public List<GameObject> allPuzzles = new List<GameObject>();
    private int activePuzzleIndex = 0;

    [Header("Events")]
    public UnityEvent OnAllPuzzlesFinished;

    [Header("Konfigurasi Sesi")]
    public SessionType sessionType;

    public enum SessionType
    {
        PreTest,
        Learning,   // Tidak perlu disimpan skornya
        PostTest
    }

    void Start()
    {
        InitializePuzzles();
    }

    void InitializePuzzles()
    {
        // 1. Matikan semua puzzle dulu (Reset)
        foreach (GameObject p in allPuzzles)
        {
            p.SetActive(false);
        }

        // 2. Nyalakan puzzle pertama (Index 0)
        activePuzzleIndex = 0;
        if (allPuzzles.Count > 0)
        {
            ActivatePuzzle(activePuzzleIndex);
        }
    }

    // Fungsi Helper untuk menyalakan puzzle & menampilkan hint
    private void ActivatePuzzle(int index)
    {
        if (index >= allPuzzles.Count) return;

        // 1. Aktifkan GameObject Puzzle
        GameObject puzzleObj = allPuzzles[index];
        puzzleObj.SetActive(true);

        // 2. Setup Hint Teks
        GridController gridScript = puzzleObj.GetComponent<GridController>();
    }

    public void OnSubmitButtonPressed()
    {

        // 1. Cek siapa yang dipanggil
        // Debug.Log(gameObject.name + ": Tombol ditekan...");

        // 2. Cek apakah dia aktif
        if (!gameObject.activeInHierarchy)
        {
            // Debug.Log(gameObject.name + ": Saya sedang tidak aktif. Abaikan.");
            return;
        }

        Debug.Log("? " + gameObject.name + " SEDANG AKTIF dan merespons tombol!");

        // 3. Cek kelengkapan Puzzle
        if (allPuzzles.Count == 0)
        {
            Debug.LogError("? ERROR: List 'All Puzzles' di " + gameObject.name + " KOSONG!");
            return;
        }

        // 4. Cek Puzzle yang sedang main
        GameObject currentPuzzleObj = allPuzzles[activePuzzleIndex];
        if (currentPuzzleObj == null)
        {
            Debug.LogError("? ERROR: Puzzle index " + activePuzzleIndex + " kosong/null!");
            return;
        }

        // 5. Cek Script Grid
        GridController activeGridScript = currentPuzzleObj.GetComponent<GridController>();

        if (activeGridScript != null)
        {
            Debug.Log("?? Mengirim perintah Submit ke: " + currentPuzzleObj.name);
            activeGridScript.OnSubmitButton();
        }
        else
        {
            Debug.LogError("? ERROR: Objek " + currentPuzzleObj.name + " tidak punya script 'Grid'!");
        }


        if (allPuzzles.Count == 0) return;
        if (!gameObject.activeInHierarchy) return;

        // Ambil puzzle yang sedang aktif
        //GameObject currentPuzzleObj = allPuzzles[activePuzzleIndex];
        //GridController activeGridScript = currentPuzzleObj.GetComponent<GridController>();

        if (activeGridScript != null)
        {
            activeGridScript.CheckAnswer();
        }

        
    }

    public void OnResetButtonPressed()
    {
        // 1. Pengaman (Wajib ada karena 1 tombol untuk 3 manager)
        if (!gameObject.activeInHierarchy) return;
        if (allPuzzles.Count == 0) return;

        // 2. Ambil Puzzle yang sedang aktif
        GameObject currentPuzzleObj = allPuzzles[activePuzzleIndex];
        GridController activeGridScript = currentPuzzleObj.GetComponent<GridController>();

        // 3. Suruh Reset
        if (activeGridScript != null)
        {
            activeGridScript.ResetPuzzle();
        }
    }

    public void AdvanceToNextPuzzle()
    {
        GameObject finishedPuzzle = allPuzzles[activePuzzleIndex];
        GridController gridScript = finishedPuzzle.GetComponent<GridController>();

        if (gridScript != null)
        {
            int score = gridScript.GetFinalScore();
            levelScores.Add(score);
            Debug.Log($"Nilai Level {activePuzzleIndex + 1}: {score}");
        }

        // 2. Matikan puzzle lama
        finishedPuzzle.SetActive(false);

        // 3. Cek Lanjut atau Selesai
        activePuzzleIndex++;

        if (activePuzzleIndex < allPuzzles.Count)
        {
            ActivatePuzzle(activePuzzleIndex);
        }
        else
        {
            FinishSession();
            Debug.Log("Tipe Sesi Saat Ini: " + sessionType.ToString());
            
            foreach (var score in levelScores)
            {
                Debug.Log(score);
            }
            
        }
    }

    void FinishSession()
    {
        // 1. Hitung Rata-Rata
        double averageScore = levelScores.Count > 0 ? levelScores.Average() : 0;
        int finalAverage = Mathf.RoundToInt((float)averageScore);

        // Variabel Data
        int preScoreVal = 0;
        int postScoreVal = 0;
        bool showPostScore = false; // Apakah angka Post ditampilkan?

        // 2. Logika Sesi
        if (sessionType == SessionType.PreTest)
        {
            PlayerPrefs.SetInt("PreTest_Score", finalAverage);
            PlayerPrefs.Save();

            preScoreVal = finalAverage;
            postScoreVal = 0;
            showPostScore = false; // Tampilkan "?"
        }
        else if (sessionType == SessionType.PostTest)
        {
            PlayerPrefs.SetInt("PostTest_Score", finalAverage);

            preScoreVal = PlayerPrefs.GetInt("PreTest_Score", 0);
            postScoreVal = finalAverage;
            showPostScore = true; // Tampilkan Angka
        }

        // 3. Panggil UI (Tanpa Title & Feedback)
        if (resultPanel != null)
        {
            resultPanel.ShowResult(
                preScoreVal,
                postScoreVal,
                showPostScore,
                () => { TriggerNextStage(); }
            );
        }
        else
        {
            TriggerNextStage();
        }
    }

    // Fungsi helper untuk bersih-bersih dan lanjut
    void TriggerNextStage()
    {
        if (OnAllPuzzlesFinished != null)
        {
            OnAllPuzzlesFinished.Invoke();
        }
        this.gameObject.SetActive(false);
    }

    public void OpenScorePanelManual()
    {
        if (resultPanel != null)
        {
            int savedPreScore = PlayerPrefs.GetInt("PreTest_Score", 0);
            int savedPostScore = PlayerPrefs.GetInt("PostTest_Score", 0);
            bool isPostDone = PlayerPrefs.HasKey("PostTest_Score");

            resultPanel.ShowResult(savedPreScore, savedPostScore, isPostDone, null);
        }
    }
}