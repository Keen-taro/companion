using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    [Header("BGM List")]
    public List<AudioClip> backgroundMusic;

    private AudioSource audioSource;
    private int currentBgmPlayed;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    private void Start()
    {
        if (backgroundMusic.Count > 0)
        {
            PlayTrack(currentBgmPlayed);
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying && backgroundMusic.Count > 0)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        currentBgmPlayed++;

        // Jika index sudah melebihi jumlah lagu (misal sudah lagu ke-3),
        // kembalikan ke 0 (Looping Playlist dari awal lagi)
        if (currentBgmPlayed >= backgroundMusic.Count)
        {
            currentBgmPlayed = 0;
        }

        PlayTrack(currentBgmPlayed);
    }

    void PlayTrack(int index)
    {
        // Masukkan klip ke AudioSource
        audioSource.clip = backgroundMusic[index];

        // Mainkan!
        audioSource.Play();
    }
}
