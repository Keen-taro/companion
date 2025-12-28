using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleporterRiddleGate : MonoBehaviour
{
    [SerializeField] private GameObject Puzzle_UI;
    [SerializeField] private List<Button> Runes = new List<Button>();
    public Teleporter teleporter;

    public void ShowPuzzle()
    {
        Puzzle_UI.SetActive(true);
    }

    public void ClosePuzzle()
    {
        Puzzle_UI.SetActive(false);
    }

    public void Correct()
    {
        teleporter.canTP = true;
        Puzzle_UI.SetActive(false);
    }

    public void Failed()
    {
        StartCoroutine(FailedPuzzleCooldown());
    }

    IEnumerator FailedPuzzleCooldown()
    {
        foreach (var rune in Runes)
        {
            rune.interactable = false;
        }

        yield return new WaitForSeconds(3f);

        foreach (var rune in Runes)
        {
            rune.interactable = true;
        }
    }

}
