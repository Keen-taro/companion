using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour, IDamageable
{
    public static PlayerSanity singleton;

    //Sanity
    public float sanity; 
    public float maxSanity; //Bar hitam pada health bar, agar terlihat seperti terisi
    float sanityPercentage;

    private Animator anim;

    //Mana
    public float mana;
    public float maxMana;
    public Image manaUI;

    //Sanity-Glass
    public GameObject[] SanityVision;

    public Rigidbody2D rb;

    private AudioSource audioSource;
    public AudioClip hitSound;

    private void Awake()
    {
        singleton = this;

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        for (int i = 1; i < SanityVision.Length; i++)
        {
            SanityVision[i].SetActive(false);
        }
    }

    private void Update()
    {
        //Health Bar fill agar terlihat terisi
        manaUI.fillAmount = mana / maxMana;
        sanityPercentage = sanity / maxSanity;

        SanityChanges();

        if (sanity >= 0)
        {

        }
    }

    public void SanityIncreases(float increase)
    {
        sanity += increase;
    }

    public void SanityDecreases(float decrease)
    {
        sanity -= decrease;
    }

    public void ManaDecrease(float manaUses)
    {
        mana -= manaUses;
    }

    private void SanityChanges()
    {
        if (sanityPercentage >= 0.8)
        {
            SanityVision[0].SetActive(true);
        }
        else if (sanityPercentage >= 0.5)
        {
            SanityVision[1].SetActive(true);
            SanityVision[0].SetActive(false);
        }
        else
        {
            SanityVision[2].SetActive(true);
            SanityVision[1].SetActive(false);
        }
    }

    public void Damage(float damageAmount)
    {
        anim.SetTrigger("Hurt");
        audioSource.PlayOneShot(hitSound);
        sanity -= damageAmount;
    }

    public void Damage(float damageAmount, float KBForce, Vector2 KBAngle)
    {
        rb.velocity = KBAngle * KBForce;
        sanity -= damageAmount;
    }
}
