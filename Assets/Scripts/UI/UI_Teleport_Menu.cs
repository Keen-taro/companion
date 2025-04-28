using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Teleport_Menu : MonoBehaviour
{
    public static UI_Teleport_Menu singelton;
    public Transform[] checkPointCollection;
    public GameObject teleporterUI;

    [SerializeField] private Animator teleportTransition;

    private void Awake()
    {
        singelton = this;
    }

    public void EnableTeleporterUI()
    {
        teleporterUI.SetActive(true);
    }

    public void DisableTeleporterUI()
    {
        teleporterUI.SetActive(false);
    }

    public void TeleportToPosition(int positionIndex)
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (positionIndex < 0 || positionIndex >= checkPointCollection.Length)
        {
            Debug.LogWarning("Invalid teleport position index");
            return;
        }

        Transform targetPosition = checkPointCollection[positionIndex];
        player.transform.position = targetPosition.position; // Move the player to the selected position
    }

    public void StartCoroutineTeleport(int positionIndex)
    {
        StartCoroutine(TeleportToAnotherPosition(positionIndex));
    }

    public IEnumerator TeleportToAnotherPosition(int positionIndex)
    {
        teleportTransition.SetTrigger("End");
        DisableTeleporterUI();

        yield return new WaitForSeconds(1f);

        TeleportToPosition(positionIndex);

        teleportTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        teleportTransition.SetTrigger("Idle");
    }
}
