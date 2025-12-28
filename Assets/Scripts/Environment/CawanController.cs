using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CawanController : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private bool isLit;
    [SerializeField] private bool insideAreaInteract;

    [Header("Recharge Settings")]
    [Tooltip("Berapa banyak energi yang diisi per detik")]
    [SerializeField] private float rechargeRate = 20f; // Misal: 20 per detik (5 detik full kalau max 100)

    private LightBehaviour wispScript; // Referensi ke script Wisp

    [Header("Components")]
    [SerializeField] private ParticleSystem fireParticle;
    [SerializeField] private AudioClip fireSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (fireParticle != null) fireParticle.Stop();

        // Otomatis cari Wisp di scene
        wispScript = FindObjectOfType<LightBehaviour>();
    }

    private void Update()
    {
        // 1. Logic Nyalakan Api (Checkpoint)
        if (insideAreaInteract && !isLit && Input.GetKeyDown(KeyCode.F))
        {
            IgniteProcess();
        }

        // 2. Logic RECHARGE (Mengisi Energi)
        // Syarat: Api nyala + Player di area + Script Wisp ditemukan
    }

    private void IgniteProcess()
    {
        isLit = true;
        if (fireParticle != null) fireParticle.Play();
        if (audioSource != null && fireSound != null)
        {
            audioSource.clip = fireSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) insideAreaInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) insideAreaInteract = false;
    }
}