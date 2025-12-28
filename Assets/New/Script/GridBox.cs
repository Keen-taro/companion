using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridBox : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Konfigurasi Tile")]
    public TileType type = TileType.Normal;
    public bool isActive;

    [Header("Audio")]
    public AudioClip activateSound;
    public AudioClip hoverSound;
    public AudioClip lockedSound;

    private AudioSource _uiAudioSource;

    [Header("Khusus Mirror Tile")]
    public GridBox partnerTile;

    [Header("Khusus Fragile Tile")]
    public bool hasBeenChanged = false;

    private Image myImage;

    [Header("Visuals")]
    // Sprites
    public Sprite voidSprite;
    public Sprite staticSprite;
    public Sprite fragileSpriteBeforeActive;
    public Sprite fragileSpriteAfterActive;

    // Colors
    public Color normalColorSprite;
    public Color hoverColorSprite;
    public Color activeColorSprite;

    public Image iconSymbolImage;


    public enum TileType
    {
        Normal,
        Void,
        Static_Ink,
        Mirror,
        Fragile
    }

    void Start()
    {
        myImage = GetComponent<Image>();

        GameObject audioObj = GameObject.Find("UIAudioManager");

        if (audioObj != null)
        {
            _uiAudioSource = audioObj.GetComponent<AudioSource>();
        }

        if (type == TileType.Mirror && partnerTile != null)
        {
            // 1. Paksa Partner jadi tipe Mirror juga (biar gak lupa ganti)
            partnerTile.type = TileType.Mirror;

            // 2. Paksa Partner menunjuk balik ke SAYA (biar gak lupa link balik)
            partnerTile.partnerTile = this;

            // 3. Update Visual Partner (biar ikonnya muncul sekarang)
            partnerTile.UpdateVisuals();
        }

        UpdateVisuals();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (type == TileType.Void || type == TileType.Static_Ink) return;

        if (type == TileType.Fragile && hasBeenChanged)
        {
            _uiAudioSource.PlayOneShot(lockedSound);
            return;
        }

        ToggleState(); 

        if (type == TileType.Mirror && partnerTile != null)
        {
            partnerTile.MirrorAction(isActive);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (type == TileType.Void || type == TileType.Static_Ink) return;

        if (_uiAudioSource != null && hoverSound != null)
            _uiAudioSource.PlayOneShot(hoverSound);

        if (!isActive)
        {
            myImage.color = hoverColorSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UpdateVisuals();
    }

    // Dipanggil oleh tile pasangan (Mirror)
    public void MirrorAction(bool newState)
    {
        if (type == TileType.Fragile && hasBeenChanged)
        {
            return;
        }

        if (type == TileType.Void)
        {
            return;
        }

        isActive = newState;

        if (type == TileType.Fragile) hasBeenChanged = true;

        UpdateVisuals();
    }

    void ToggleState()
    {
        isActive = !isActive;

        if (type == TileType.Fragile) hasBeenChanged = true;

        UpdateVisuals();

        if (_uiAudioSource != null && activateSound != null)
            _uiAudioSource.PlayOneShot(activateSound);
    }

    public void ResetGridSquare()
    {
        // 1. Reset status Active
        if (type == TileType.Static_Ink)
        {
            isActive = true; // Static selalu hitam
        }
        else
        {
            isActive = false; // Sisanya jadi putih
        }

        // 2. Reset status Fragile (PENTING: Biar bisa diklik lagi)
        hasBeenChanged = false;

        // 3. Update tampilan
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        myImage.color = normalColorSprite;

        switch (type)
        {
            case TileType.Void:
                myImage.sprite = voidSprite;
                break;

            case TileType.Static_Ink:
                myImage.sprite = staticSprite;
                break;

            case TileType.Mirror:
                // Tampilkan Ikon
                if (iconSymbolImage != null)
                {
                    if (!iconSymbolImage.gameObject.activeSelf)
                        iconSymbolImage.gameObject.SetActive(true);

                    if (isActive)
                        iconSymbolImage.color = normalColorSprite; 
                    else
                        iconSymbolImage.color = activeColorSprite; 
                }

                goto default;

            default:
                // Pewarnaan Background (Ink/Normal)
                if (isActive)
                    myImage.color = activeColorSprite; // Hitam
                else
                    myImage.color = normalColorSprite; // Putih

                // Khusus Fragile (Sprite Retak)
                if (type == TileType.Fragile)
                {
                    if (!hasBeenChanged)
                    {
                        // Kondisi Awal: Retak Tipis
                        myImage.sprite = fragileSpriteBeforeActive;
                    }
                    else
                    {
                        // Kondisi Akhir: Gembok / Pecah / Terkunci
                        myImage.sprite = fragileSpriteAfterActive;
                        myImage.color = Color.white;
                    }
                }
                break;
        }
    }
}