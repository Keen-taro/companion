using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oracle : MonoBehaviour
{
    public int oracleID;  // Unique identifier for the oracle
    public bool isLit = false;  // Tracks if the oracle is turned on or off
    public ParticleSystem particleEffect;  // Particle effect for lighting up the oracle
    public Collider2D colliderBox;

    private AudioSource audioSource;
    [SerializeField] private AudioClip litSound;
    [SerializeField] private OracleManager oracleManager;

    public float cooldownTime = 2f; // Cooldown duration in seconds
    private bool isCooldownActive = false;

    private void Awake()
    {
        particleEffect = GetComponent<ParticleSystem>();
        colliderBox = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

        isLit = false;
        particleEffect.Stop();
    }

    private void Start()
    {
        colliderBox.enabled = false;
    }

    // Call this to toggle the oracle's state
    public void ToggleOracle()
    {
        if (isLit && !isCooldownActive)
        {
            // Turn off the oracle and remove from the array
            oracleManager.RemoveOracle(oracleID);
            Dim();
            StartCoroutine(StartCooldown());
        }
        else if (!isLit && !isCooldownActive)
        {
            // Turn on the oracle and add to the array
            oracleManager.AddOracle(this);
            LightUp();
            StartCoroutine(StartCooldown());
        }
    }

    // Turn the oracle on and play the particle effect
    public void LightUp()
    {
        isLit = true;
        UpdateVisual();
    }

    // Turn the oracle off and stop the particle effect
    public void Dim()
    {
        isLit = false;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (particleEffect != null)
        {
            //Debug.Log("No Particle Effect");

            if (isLit)
            {
                particleEffect.Play();  // Activate particle effect when lit
                //Debug.Log("Particle on");
            }
            else
            {
                particleEffect.Stop();  // Deactivate particle effect when dimmed
                //Debug.Log("Particle off");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ToggleOracle();
        }
    }

    public void Reset()
    {
        isLit = false;
        particleEffect.Stop();
    }

    public bool ChecksOracleLit()
    {
        return isLit;
    }

    public void PlayLitSound()
    {
        if (litSound != null)
        {
            audioSource.PlayOneShot(litSound);
        }
    }

    private IEnumerator StartCooldown()
    {
        isCooldownActive = true; // Activate cooldown
        yield return new WaitForSeconds(cooldownTime); // Wait for cooldown duration
        isCooldownActive = false; // Deactivate cooldown
    }
}

