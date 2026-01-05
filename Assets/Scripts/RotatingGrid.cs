using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGrid : MonoBehaviour
{
    [SerializeField] List<GameObject> Grid = new List<GameObject>();
    [SerializeField] List<GameObject> Hint = new List<GameObject>();

    private bool isPlayerInRange;
    public bool isWhispInArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayerInRange = false;
    }

    private void Update()
    {
        // Pemicu: Pemain dalam jangkauan DAN tekan tombol F
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && isWhispInArea)
        {
            RotateItems();
        }
    }

    void RotateItems()
    {
        // 1. Putar list GRID masing-masing
        foreach (GameObject item in Grid)
        {
            if (item != null && item.activeInHierarchy)
            {
                // Berputar 90 derajat pada sumbu Z lokalnya sendiri
                item.transform.Rotate(0f, 0f, 90f);
            }
        }

        // 2. Putar list HINT masing-masing
        foreach (GameObject item in Hint)
        {
            if (item != null && item.activeInHierarchy)
            {
                // Berputar 90 derajat pada sumbu Z lokalnya sendiri
                item.transform.Rotate(0f, 0f, 90f);
            }
        }
    }
}
