using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhispAreaRotateGrid : MonoBehaviour
{
    [SerializeField] RotatingGrid oracle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Whisp"))
        {
            oracle.isWhispInArea = true;
            Debug.Log("Whisp in Area");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Whisp"))
        {
            oracle.isWhispInArea = false;
            Debug.Log("Whisp is not in Area");
        }
    }
}
