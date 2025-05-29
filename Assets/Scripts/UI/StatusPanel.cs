using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusPanel : MonoBehaviour
{
    public BubbleMemoryController bubbleMemoryController;

    public TextMeshProUGUI displayText;
    public TextMeshProUGUI totalMemoryCollected;
    public List<Timer> puzzleListAndTime;

    private void Awake()
    {
        // Inisialisasi referensi
        if (displayText == null)
            Debug.LogError("Text reference belum di-set!");
    }

    private void Start()
    {
        // Nonaktifkan panel setelah inisialisasi
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // Update UI setiap kali panel dibuka
        if (puzzleListAndTime != null)
            UpdatePuzzleListDisplay();
    }

    // Dipanggil dari script lain untuk mengisi data
    public void SetTimerData(List<Timer> timers)
    {
        puzzleListAndTime = timers;
    }

    private void UpdatePuzzleListDisplay()
    {
        displayText.text = "";
        foreach (Timer timer in puzzleListAndTime)
        {
            displayText.text += $"{timer.displayName}: " +
                $"{timer.minuteClock:00}:{timer.secondClock:00} " +
                $"Attempt: {timer.attempCount}\n\n";
        }

        totalMemoryCollected.text = (bubbleMemoryController.collectedMemory + "  /  " + bubbleMemoryController.bubbleMemory.Length);
    }

    public void ContinueTheGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
}
