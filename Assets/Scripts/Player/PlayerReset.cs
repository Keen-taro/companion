using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    public Transform resetPlayerPosition;
    public PlayerStateMachine player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && player.canMove)
        {
            transform.position = resetPlayerPosition.position;
        }
    }
}
