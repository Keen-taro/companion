using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSquare : MonoBehaviour
{
    public Image occupiedImage;

    private void Start()
    {
        occupiedImage.gameObject.SetActive(false);
    }

    public void DeactivateShape()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }

    public void ActivateShape()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.SetActive(true);
    }

    // Di dalam class ShapeSquare
    public bool IsOnValidGridPosition()
    {
        // Implementasi deteksi grid yang valid
        // Contoh sederhana:
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
    
        foreach (var collider in colliders)
        {
            GridSquare gridSquare = collider.GetComponent<GridSquare>();
            if (gridSquare != null && gridSquare.CanWeUseThisSquare())
            {
                return true;
            }
        }
        return false;
    }
}
