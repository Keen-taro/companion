using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTraps : MonoBehaviour
{
    [SerializeField] private GameObject spikeSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spikeSprite.transform.localPosition += new Vector3(0, 2, 0);
            PlayerStateMachine player = other.GetComponent<PlayerStateMachine>();
            player.SwitchState(player.deathState);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spikeSprite.transform.localPosition -= new Vector3(0, 2, 0);
        }
    }
}
