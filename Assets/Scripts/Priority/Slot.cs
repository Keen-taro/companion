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
    public SpriteRenderer sprite;

    [Header("Slot State")]
    public SlotState state = SlotState.Empty;

    [Header("Colors")]
    public Color baseColor = Color.white;
    public Color occupiedColor = Color.green;
    public Color mixedColor = Color.magenta;
    public Color invalidColor = Color.red;

    private void Awake()
    {
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Mengatur slot sebagai terisi. Jika sudah terisi, ubah jadi Mixed.
    /// </summary>
    public void SetOccupied(Color pieceColor)
    {
        if (state == SlotState.Empty)
        {
            sprite.color = pieceColor;
            state = SlotState.Occupied;
        }
        else if (state == SlotState.Occupied)
        {
            sprite.color = mixedColor;
            state = SlotState.Mixed;
        }
    }

    /// <summary>
    /// Tandai slot sebagai invalid (salah saat submit).
    /// </summary>
    public void SetInvalid()
    {
        sprite.color = invalidColor;
        state = SlotState.Invalid;
    }

    /// <summary>
    /// Reset slot ke keadaan kosong.
    /// </summary>
    public void Clear()
    {
        if (state == SlotState.Empty)
        {
            state = SlotState.Empty;
            sprite.color = baseColor;
        }
    }

    /// <summary>
    /// Perbarui warna sesuai status saat ini.
    /// </summary>
    public void UpdateVisual()
    {
        switch (state)
        {
            case SlotState.Empty:
                sprite.color = baseColor;
                break;
            case SlotState.Occupied:
                sprite.color = occupiedColor;
                break;
            case SlotState.Mixed:
                sprite.color = mixedColor;
                break;
            case SlotState.Invalid:
                sprite.color = invalidColor;
                break;
        }
    }
}
