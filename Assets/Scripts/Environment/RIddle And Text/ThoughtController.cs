using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtController : MonoBehaviour
{
    public Naration naration;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            collision.GetComponent<PlayerStateMachine>().isReading = true;

            DialogueController.Instance.ShowNaration(naration, () =>
            {
                collision.GetComponent<PlayerStateMachine>().isReading = false;
                Destroy(gameObject);
            });
        }
    }
}
