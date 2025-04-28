using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenSpaceController : MonoBehaviour
{
    [SerializeField]
    private Transform teleportPoint;
    private PlayerStateMachine player;
    private bool playerInArea;

    private Transform playerPosition;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerStateMachine>();
            playerPosition = other.GetComponent<Transform>();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInArea)
        {
            StartCoroutine(EnterMindRealm());
        }
    }

    IEnumerator EnterMindRealm()
    {
        // Play the transition
        // Player diasble movement

        yield return new WaitForSeconds(1.5f);

        player.transform.position = teleportPoint.position;

        yield return new WaitForSeconds(1f);

        //Play the end transition animation (open screen)
        //Enable player movement
    }
}
