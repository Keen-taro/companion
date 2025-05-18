using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenSpaceController : MonoBehaviour
{
    public static BrokenSpaceController singleton;

    [SerializeField]
    private Transform teleportPointOne, teleportPointTwo;

    private void Awake()
    {
        singleton = this;
    }

    public void TeleportToTwo(Transform playerPosition)
    {
        playerPosition.position = teleportPointTwo.position;
    }

    public void TeleportToOne(Transform playerPosition)
    {
        playerPosition.position = teleportPointOne.position;
    }

    
}
