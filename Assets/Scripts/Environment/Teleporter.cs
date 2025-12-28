using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter : MonoBehaviour
{
    [Header("Other Can Be Empty Except Teleport Config")]

    [Header("Teleport Config || Change This If Only TP Player")]
    public Transform targetTeleporter;
    private Transform playerPosition;

    [Header("Dialogue Element")]
    [SerializeField] private GameObject textBox;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Whisper N Whisp Conversation")]
    [SerializeField] private string whisper;
    [SerializeField] private List<string> afterWhisperDialogue = new List<string>();

    [Header("Typing Settings")]
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float delayAfterType = 1f;

    [Header("Puzzle Gate")]
    [SerializeField] private TeleporterRiddleGate gatePuzzle;

    private bool playerInArea;
    public bool canTP;
    private float tpCooldownTime = 1f, tpReady;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPosition = other.transform;
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInArea = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInArea && Time.time >= tpReady && targetTeleporter != null)
        {
            if (gatePuzzle != null && !canTP)
            {
                gatePuzzle.ShowPuzzle();
                return;
            }

            playerPosition.position = targetTeleporter.position;
            tpReady = Time.time + tpCooldownTime;

            if (whisper != null)
            {
                StartCoroutine(WhisperSequence());
            }
        }
    }

    IEnumerator WhisperSequence()
    {
        // 1. Tampilkan bisikan
        yield return StartCoroutine(TypeLine(whisper));

        // 2. Tunggu sedikit sebelum lanjut
        yield return new WaitForSeconds(1f);

        // 3. Lanjutkan dialog whisp setelah teleport
        for (int i = 0; i < afterWhisperDialogue.Count; i++)
        {
            yield return StartCoroutine(TypeLine(afterWhisperDialogue[i]));
            yield return new WaitForSeconds(0.5f);
        }

        // 4. Sembunyikan text box setelah selesai
        textBox.SetActive(false);
        text.text = string.Empty;
    }

    IEnumerator TypeLine(string message)
    {
        textBox.SetActive(true);
        text.text = string.Empty;

        foreach (char c in message)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(delayAfterType);
    }
}