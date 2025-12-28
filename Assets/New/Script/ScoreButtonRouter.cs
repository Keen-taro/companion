using UnityEngine;

public class ScoreButtonRouter : MonoBehaviour
{
    [Header("Masukkan Semua Manager")]
    public PuzzleController preTestManager;
    public PuzzleController learningManager;
    public PuzzleController postTestManager;

    public void OnClick()
    {
        // 1. Cek Prioritas: Mana yang lagi Aktif?
        if (preTestManager.gameObject.activeInHierarchy)
        {
            preTestManager.OpenScorePanelManual();
        }
        else if (learningManager.gameObject.activeInHierarchy)
        {
            learningManager.OpenScorePanelManual();
        }
        else if (postTestManager.gameObject.activeInHierarchy)
        {
            postTestManager.OpenScorePanelManual();
        }
        // 2. DEFAULT (Jika semua mati/sedang dialog)
        else
        {
            Debug.Log("Semua Manager Mati. Menggunakan Default (PreTest).");
            // Kita paksa PreTestManager yang buka, karena datanya sama aja (dari PlayerPrefs)
            // Syaratnya: PreTestManager harus sudah di-assign ResultPanel-nya di Inspector
            preTestManager.OpenScorePanelManual();
        }
    }
}