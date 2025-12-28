using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtController : MonoBehaviour
{
    public TextMeshProUGUI dialogueTextElement; // Dialogue Box

    private void Update()
    {
        
    }

    void StartDialogue(string text)
    {
        StartCoroutine(TypeLine(text));
    }

    IEnumerator TypeLine(string text)
    {
        foreach (char c in text.ToCharArray())
        {
            dialogueTextElement.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            collision.GetComponent<PlayerStateMachine>().isReading = true;

            DialogueController.Instance.ShowNaration(naration, () =>
            {
                collision.GetComponent<PlayerStateMachine>().isReading = false;
                Destroy(gameObject);
            });

            BubbleMemoryController.singleton.CollectedMemory();
        }
        */

        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {
            BubbleMemoryController.singleton.CollectedMemory();
            Destroy(gameObject);
        }   
    }
}
