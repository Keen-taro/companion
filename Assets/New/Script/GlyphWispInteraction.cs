using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphWispInteraction : MonoBehaviour
{
    [SerializeField] GameObject hint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Cek apakah ada sesuatu yang masuk (apapun itu)
        Debug.Log("Sesuatu menabrak trigger: " + collision.gameObject.name);

        // 2. Cek tag objek yang masuk
        Debug.Log("Tag objek tersebut adalah: " + collision.tag);

        if (collision.CompareTag("Whisp")) // Pastikan ejaan di sini sama dengan log di atas
        {
            Debug.Log("Tag Cocok! Menyalakan hint.");
            if (hint != null)
            {
                hint.SetActive(true);
            }
            else
            {
                Debug.LogError("Variable 'hint' belum di-assign di Inspector!");
            }
        }
    }
}
