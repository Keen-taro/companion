using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphWispInteraction : MonoBehaviour
{
    [SerializeField] GameObject hint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Whisp"))
        {
            hint.SetActive(true);
        }
    }
}
