using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultPanelDisplay : MonoBehaviour
{
    [Header("Score Labels")]
    public TextMeshProUGUI preScoreText;   // Teks Angka Pre-Test
    public TextMeshProUGUI postScoreText;  // Teks Angka Post-Test

    [Header("Isi Link di Sini")]
    [Tooltip("Jangan lupa pakai https:// di depannya")]
    public string url;

    private System.Action onContinueCallback;



    // Fungsi jadi lebih pendek parameternya
    public void ShowResult(int preScore, int postScore, bool isPostTestDone, System.Action onContinue)
    {
        if (preScore == 0)
        {
            preScoreText.text = $"Pre-Test\n<size=150%><color=white>-</color></size>";
        }
        else
        {
            preScoreText.text = $"Pre-Test\n<size=150%><color=white>{preScore}</color></size>";
        }

        this.gameObject.SetActive(true);
        onContinueCallback = onContinue;

        // ... (Logika Pre/Post Score Teks SAMA SEPERTI SEBELUMNYA) ...
        preScoreText.text = $"Pre-Test\n<size=150%><color=black>{preScore}</color></size>";

        if (isPostTestDone)
            postScoreText.text = $"Post-Test\n<size=150%><color=white>{postScore}</color></size>";
        else
            postScoreText.text = $"Post-Test\n<size=150%><color=white>?</color></size>";
    }

    public void OnContinuePressed()
    {
        this.gameObject.SetActive(false);
        if (onContinueCallback != null) onContinueCallback.Invoke();
    }

    public void OpenLink()
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogError("URL kosong! Isi dulu di Inspector.");
            return;
        }

        // Ini perintah untuk membuka browser
        Application.OpenURL(url);

        Debug.Log("Membuka browser ke: " + url);
    }
}