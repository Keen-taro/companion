using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// TAMBAHKAN 'IPointerClickHandler' DI SINI
public class GridSquare : MonoBehaviour, IPointerClickHandler
{
    public Image activeImage;  
    public Image normalImage;
    public Image disableImage;

    public bool isActivate;
    public bool disableGrid;
    public bool inCutscene;
    public bool alreadyComplete;


    void Start()
    {
        // Panggil ResetGridSquare agar status awalnya bersih
        isActivate = false;
        ResetGridSquare();

        if (disableGrid && disableImage != null)
        {
            disableImage.gameObject.SetActive(true);
        }
    }

    public void DisableGrid()
    {
        alreadyComplete = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isActivate = !isActivate;

        if (disableGrid) return;

        if (isActivate)
        {
            // Tampilkan status "ON"
            normalImage.gameObject.SetActive(false);
            activeImage.gameObject.SetActive(true);
        }
        else
        {
            // Tampilkan status "OFF"
            normalImage.gameObject.SetActive(true);
            activeImage.gameObject.SetActive(false);
        }
    }

    public void ResetGridSquare()
    {

        if (disableGrid) return;

        isActivate = false;
        // Selected = false; // Hapus
        normalImage.gameObject.SetActive(true);
        // hoverImage.gameObject.SetActive(false); // Hapus
        activeImage.gameObject.SetActive(false);
    }
}