using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Timer timerPuzzle;
    public Stone relatedStone;

    public ShapeStorage shapeStorage;
    public List<GameObject> _gridSquare = new List<GameObject>();

    public List<GridSquare> mainGrid = new List<GridSquare>();
    public List<GridSquare> solutionGrid = new List<GridSquare>();


    // Untuk event listener (contoh):
    void OnEnable()
    {
        GameEvents.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;
        GameEvents.OnPuzzleSolved += HandlePuzzleSolved;
        GameEvents.OnPuzzleFailed += HandlePuzzleFailed;
    }

    void OnDisable()
    {
        GameEvents.CheckIfShapeCanBePlaced -= CheckIfShapeCanBePlaced;
        GameEvents.OnPuzzleSolved -= HandlePuzzleSolved;
        GameEvents.OnPuzzleFailed -= HandlePuzzleFailed;
    }

    private void HandlePuzzleSolved()
    {
        // Play victory sound/effect
    }

    private void HandlePuzzleFailed(int errorCount)
    {
        // Show error message
    }

    private void CheckIfShapeCanBePlaced()
    {
        foreach (var square in _gridSquare)
        {
            var gridSquare = square.GetComponent<GridSquare>();

            if (gridSquare.CanWeUseThisSquare() == true)
            {
                gridSquare.ActivateSquare();
            }
        }

        shapeStorage.GetCurrentSelectedShape().DeactivateShape();
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

        bool isCorrect = true;
        int incorrectCount = 0;
        List<int> incorrectIndices = new List<int>();

        // 2. Pengecekan utama
        for (int i = 0; i < mainGrid.Count; i++)
        {
            bool mainOccupied = mainGrid[i].SquareOccupied;
            bool solutionOccupied = solutionGrid[i].SquareOccupied;

            if (mainOccupied != solutionOccupied)
            {
                isCorrect = false;
                incorrectCount++;
                incorrectIndices.Add(i);

                // Beri feedback visual pada grid yang salah
                mainGrid[i].ShowIncorrectFeedback();
            }
            else
            {
                // Beri feedback visual pada grid yang benar
                mainGrid[i].ShowCorrectFeedback();
            }
        }

        // 3. Hasil pengecekan
        if (isCorrect)
        {
            Debug.Log("Jawaban benar!");
            relatedStone.FinishThePuzzle();
        }
        else if(!isCorrect)
        {
            Debug.Log($"Jawaban salah! {incorrectCount} kesalahan pada grid: {string.Join(",", incorrectIndices)}");
            Debug.Log("Resetting the Shape");
            shapeStorage.ResetAllShapes();
            ResetAllGridSquares();
            timerPuzzle.AddAttemp();
        }
    }

    public void ResetAllGridSquares()
    {
        foreach (Transform child in transform)
        {
            GridSquare square = child.GetComponent<GridSquare>();
            if (square != null)
            {
                square.ResetGridSquare();
                child.gameObject.SetActive(true); // Pastikan grid square aktif
            }
        }
    }
}
