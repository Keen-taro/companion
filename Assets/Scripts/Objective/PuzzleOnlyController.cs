using UnityEngine;
using System.Collections.Generic;

public class PuzzleOnlyController : MonoBehaviour
{
    public List<GameObject> EveryGridPuzzle = new List<GameObject>();

    // 'currentPuzzle' sekarang private agar lebih aman
    private int currentPuzzleIndex = 0;

    private void Start()
    {
        // 1. Pastikan semua puzzle non-aktif di awal
        for (int i = 0; i < EveryGridPuzzle.Count; i++)
        {
            EveryGridPuzzle[i].SetActive(false);
        }

        // 2. Aktifkan HANYA puzzle pertama
        if (EveryGridPuzzle.Count > 0)
        {
            EveryGridPuzzle[currentPuzzleIndex].SetActive(true);
        }
    }

    public void NextPuzzle()
    {
        if (currentPuzzleIndex + 1 < EveryGridPuzzle.Count)
        {
            EveryGridPuzzle[currentPuzzleIndex].SetActive(false);

            currentPuzzleIndex++;

            EveryGridPuzzle[currentPuzzleIndex].SetActive(true);
        }
        else
        {
            Debug.Log("Selesai semua puzzle!");
            EveryGridPuzzle[currentPuzzleIndex].SetActive(false);
        }
    }
}