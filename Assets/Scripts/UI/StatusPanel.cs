using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusPanel : MonoBehaviour
{
    public List<Timer> puzzleListAndTime = new List<Timer>();
    public TextMeshProUGUI displayText;

    private bool timeStart;
    private float totalTime = 1205f;

    private void Update()
    {
        for (int i = 0; i < puzzleListAndTime.Count; i++)
        {
            displayText.text += $"{puzzleListAndTime[i].displayName}: + " +
                $"{puzzleListAndTime[i].minuteClock}:{puzzleListAndTime[i].secondClock:F0} +" +
                    $"Attemp : {puzzleListAndTime[i].attempCount}\n \n";
        }

        if (timeStart && totalTime >= 0) //Game Start
        {
            totalTime -= Time.deltaTime;
        }

        if (totalTime == 0)
        {
            gameObject.SetActive(true);
        }
    }

    private void StartTimeToDisplay()
    {
        timeStart = true; 
    }

    public void ContinueTheGame()
    {
        gameObject.SetActive(false);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
}
