using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LightGridHelp : MonoBehaviour
{
    [Header("Referensi Objek")]
    public Grid mainGrid; // Referensi ke script Grid utama
    public List<GridSquare> thirdGridPuzzle = new List<GridSquare>(); // List grid yang akan dianimasikan
    public List<string> cutsceneWhisp = new List<string>(); // List dialog untuk cutscene
    public TextMeshProUGUI dialogueTextElement; // UI Text untuk dialog
    public Image blocked; // Gambar yang menghalangi

    [Header("Pengaturan Waktu")]
    [SerializeField] private float delayBeforeStart = 5f;
    [SerializeField] private float activationDelay = 1.5f;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float delayAfterType = 1f;

    // Variabel privat untuk mengelola status
    private bool cooldown = false;
    private bool hasStartedThirdGrid = false;
    private int sequence = 0; // Melacak dialog

    /// <SAYA>
    /// Update() tetap dipakai sesuai skenario Anda:
    /// Memicu (trigger) cutscene saat hint terakhir dari puzzle SEBELUMNYA muncul.
    /// </SAYA>
    private void Update()
    {
        // Pengecekan keamanan jika mainGrid belum di-set
        if (mainGrid == null) return;

        // Trigger: Saat hint terakhir muncul dan cutscene belum dimulai
        if (!hasStartedThirdGrid && mainGrid.currentStep == mainGrid.dialogueHints.Count - 1)
        {
            hasStartedThirdGrid = true;
            StartCoroutine(WaitForStart());
        }
    }

    /// <SAYA>
    /// Coroutine ini menunggu 5 detik, lalu memulai animasi dan dialog
    /// secara BERSAMAAN (sesuai skenario Anda).
    /// </SAYA>
    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        // Memulai animasi grid (akan berjalan di background)
        StartCoroutine(ActivateInSequence());

        // Memulai sekuens dialog (juga berjalan di background)
        StartCoroutine(RunDialogueSequence());
    }

    /// <SAYA>
    /// Coroutine ini meng-animasikan grid jawaban.
    /// Kode Anda di sini sudah logis.
    /// </SAYA>
    IEnumerator ActivateInSequence()
    {
        for (int i = 0; i < thirdGridPuzzle.Count; i++)
        {
            if (thirdGridPuzzle[i] != null) // Pengecekan keamanan
            {
                // Asumsi 'isActivate' dan 'DisableGrid' adalah fungsi di 'GridSquare.cs'
                thirdGridPuzzle[i].isActivate = true;
                thirdGridPuzzle[i].normalImage.gameObject.SetActive(false);
                thirdGridPuzzle[i].activeImage.gameObject.SetActive(true);
                thirdGridPuzzle[i].disableGrid = true;
            }
            yield return new WaitForSeconds(activationDelay);
        }

        if (blocked != null)
        {
            blocked.gameObject.SetActive(false);
        }
    }

    /// <SAYA>
    /// PERBAIKAN KRITIS: Ini menggantikan 'FirstTimeOpenTheThirdPuzzleDialogue'.
    /// Ini menggunakan 'while' (bukan 'if') agar SEMUA dialog muncul,
    /// dan 'WaitUntil' untuk menunggu 'cooldown' selesai.
    /// </SAYA>
    IEnumerator RunDialogueSequence()
    {
        sequence = 0; // Selalu mulai dari dialog pertama

        while (sequence < cutsceneWhisp.Count)
        {
            // TUNGGU sampai coroutine 'TypeLine' sebelumnya selesai (cooldown = false)
            yield return new WaitUntil(() => !cooldown);

            // Mulai 'TypeLine' untuk dialog berikutnya
            StartCoroutine(TypeLine(cutsceneWhisp[sequence]));

            sequence++;
        }

        // --- TAMBAHAN BARU (DIMULAI DARI SINI) ---

        // Setelah semua dialog cutscene (cutsceneWhisp) selesai,
        // kita kembalikan dialog box ke hint terakhir dari mainGrid.

        // 1. Tunggu dulu 'cooldown' dari dialog cutscene terakhir selesai.
        yield return new WaitUntil(() => !cooldown);

        // 2. Cek apakah mainGrid dan list hint-nya ada
        if (mainGrid != null && mainGrid.dialogueHints.Count > 0)
        {
            // 3. Ambil index dari hint terakhir (yang memicu kita)
            int lastHintIndex = mainGrid.dialogueHints.Count - 1;

            // 4. Ambil teks hint terakhir itu
            string lastMainHint = mainGrid.dialogueHints[lastHintIndex];

            // 5. Tampilkan kembali hint itu menggunakan efek ketik
            StartCoroutine(TypeLine(lastMainHint));
        }

        // --- SELESAI TAMBAHAN ---
    }

    /// <SAYA>
    /// Coroutine efek ketik Anda.
    /// Kode Anda di sini sudah logis dan benar.
    /// </SAYA>
    IEnumerator TypeLine(string text)
    {
        cooldown = true;
        dialogueTextElement.text = string.Empty;

        foreach (char c in text.ToCharArray()) // .ToCharArray() lebih aman
        {
            dialogueTextElement.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(delayAfterType);
        cooldown = false;
    }
}
