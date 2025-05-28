using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterAddAttemp : MonoBehaviour
{
    public Timer stoneTimerRefrence;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            stoneTimerRefrence.AddAttemp();
        }
    }
}
