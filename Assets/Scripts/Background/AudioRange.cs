using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRange : MonoBehaviour
{
    public Transform player;
    public AudioSource audioSource;
    public float maxDistance = 10f;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= maxDistance)
        {
            audioSource.volume = 1 - (distance / maxDistance);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}

