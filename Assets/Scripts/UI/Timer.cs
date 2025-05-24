using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public string displayName = "";

    private float currentTime;

    public float secondClock = 0f;
    public float minuteClock = 0f;
    public float attempCount;

    private bool playerStartingThePuzzle = false;
    private bool playerCompletedThePuzzle;

    public TextMeshProUGUI timePass;
    public TextMeshProUGUI attempCountText;


    // Update waktu (contoh)
    void Update()
    {
        if (!playerCompletedThePuzzle && playerStartingThePuzzle)
        {
            currentTime += Time.deltaTime;
            minuteClock = Mathf.FloorToInt(currentTime / 60);
            secondClock = Mathf.FloorToInt(currentTime % 60);
            // Format waktu menjadi "00:00"
        }

        timePass.text = $"{minuteClock:00}:{secondClock:00}";
        attempCountText.text = attempCount.ToString();
    }

    public void StartTimer()
    {
        playerStartingThePuzzle = true;
    }

    public void CompleteThePuzzle()
    {
        playerCompletedThePuzzle = true;
    }

    public void AddAttemp()
    {
        attempCount++;
    }

    public void ResetTimeWhenClose()
    {
        secondClock = 0f;
        minuteClock = 0f;
        AddAttemp();
    }
}
