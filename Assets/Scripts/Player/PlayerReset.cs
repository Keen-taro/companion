using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    public Transform resetPlayerPosition;
    public PlayerStateMachine player;
    public Player playerState;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = resetPlayerPosition.position;

            //MovingPlatform.singleton.ResetPlatform();

            player.ResetState();
        }
    }
}
