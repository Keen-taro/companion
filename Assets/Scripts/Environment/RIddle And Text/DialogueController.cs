using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance;

    public TextMeshProUGUI dialogueText;
    public Image darkBackground;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowNaration(Naration naration, System.Action onComplete)
    {
        StartCoroutine(TypeText(naration.narationText, onComplete));
    }

    private IEnumerator TypeText(string text, System.Action onComplete)
    {
        darkBackground.gameObject.SetActive(true);
        darkBackground.CrossFadeAlpha(0.8f, 0.3f, false);
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            if (c == '\n')
                yield return new WaitForSeconds(0.2f); // extra delay on newlines
            else
                yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));

        dialogueText.gameObject.SetActive(false);
        darkBackground.CrossFadeAlpha(0f, 0.3f, false);
        yield return new WaitForSeconds(0.3f);
        darkBackground.gameObject.SetActive(false);

        onComplete?.Invoke();
    }
}
