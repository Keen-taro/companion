using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtController : MonoBehaviour
{
    public Naration naration; // assign different text per collectible
    public TextMeshProUGUI dialogueText;
    public Image darkBackground;
    PlayerStateMachine player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            player = collision.GetComponent<PlayerStateMachine>();

            player.isReading = true;
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        darkBackground.gameObject.SetActive(true);
        darkBackground.CrossFadeAlpha(0.8f, 0.3f, false);
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "";

        foreach (char c in naration.narationText)
        {
            if (".!?".Contains(c))
                yield return new WaitForSeconds(0.15f);
            else if (c == '\n')
                yield return new WaitForSeconds(0.25f);
            else
                yield return new WaitForSeconds(0.03f);
        }

        // Wait for player to press a key
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));

        dialogueText.gameObject.SetActive(false);
        darkBackground.CrossFadeAlpha(0f, 0.3f, false);

        yield return new WaitForSeconds(0.3f); // Let fade finish
        darkBackground.gameObject.SetActive(false);

        player.isReading = false;

        Destroy(gameObject); // remove the collectible
    }
}
