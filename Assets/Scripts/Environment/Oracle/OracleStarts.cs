using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OracleStarts : MonoBehaviour
{
    public Timer timerOracle;
    public GameObject textTime;
    private bool isStarting;

    [SerializeField] private OracleManager oracleManager;
    private ParticleSystem particleEffect;

    private AudioSource audioSource;
    [SerializeField] private AudioClip inAreaSound;

    private void Awake()
    {
        particleEffect = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        particleEffect.Stop();
    }

    public void PlaySound()
    {
        if (inAreaSound != null)
        {
            audioSource.PlayOneShot(inAreaSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlaySound();
            particleEffect.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isStarting && !oracleManager.CheckIfComplete())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("oracle is singing");
                oracleManager.StartPuzzle();
                textTime.SetActive(true);
                timerOracle.AddAttemp();
                timerOracle.StartTimer();

                isStarting = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !oracleManager.CheckIfComplete() && !isStarting)
        {
            Debug.Log("Leaving Area Start");
            particleEffect.Stop();
        }
    }
}
