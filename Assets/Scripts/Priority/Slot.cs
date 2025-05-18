using UnityEngine;
using UnityEngine.UI;

public enum SlotState
{
    Empty,
    Occupied,
    Mixed,
    Invalid
}

public class Slot : MonoBehaviour
{
    public Image image;

    [Header("Slot State")]
    public SlotState state = SlotState.Empty;

    [Header("Colors")]
    public Color baseColor = Color.white;
    public Color occupiedColor = Color.green;
    public Color mixedColor = Color.magenta;
    public Color invalidColor = Color.red;

    private void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();
    }

    /// <summary>
    /// Mengatur slot sebagai terisi. Jika sudah terisi, ubah jadi Mixed.
    /// </summary>
    public void SetOccupied(Color pieceColor)
    {
        if (state == SlotState.Empty)
        {
            image.color = pieceColor;
            state = SlotState.Occupied;
        }
        else if (state == SlotState.Occupied)
        {
            image.color = mixedColor;
            state = SlotState.Mixed;
        }
    }

    /// <summary>
    /// Tandai slot sebagai invalid (salah saat submit).
    /// </summary>
    public void SetInvalid()
    {
        image.color = invalidColor;
        state = SlotState.Invalid;
    }

    /// <summary>
    /// Reset slot ke keadaan kosong.
    /// </summary>
    public void Clear()
    {
        state = SlotState.Empty;
        image.color = baseColor;
    }

    /// <summary>
    /// Perbarui warna sesuai status saat ini.
    /// </summary>
    public void UpdateVisual()
    {
        switch (state)
        {
            case SlotState.Empty:
                image.color = baseColor;
                break;
            case SlotState.Occupied:
                image.color = occupiedColor;
                break;
            case SlotState.Mixed:
                image.color = mixedColor;
                break;
            case SlotState.Invalid:
                image.color = invalidColor;
                break;
        }
    }
}
