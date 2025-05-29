using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterAddAttemp : MonoBehaviour
{
    public Timer stoneTimerRefrence;
    private bool wrongTeleporter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && wrongTeleporter)
        {
            stoneTimerRefrence.AddAttemp();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wrongTeleporter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        wrongTeleporter = false;
    }
}
