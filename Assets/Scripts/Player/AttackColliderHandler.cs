using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderHandler : MonoBehaviour
{
    private float damage;
    private Vector2 KBAngle;
    private float KBForce;

    public Animator animFX;
    public AudioSource audioSource;
    public AudioClip hitSound;

    private void Awake()
    {
        audioSource = audioSource.GetComponent<AudioSource>();
    }

    public void SetAttackProperties(float damage, Vector2 KBAngle, float KBForce)
    {
        this.damage = damage;
        this.KBAngle = KBAngle;
        this.KBForce = KBForce;
    }

    public void SetAttackProperties(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            animFX.SetTrigger("Stab_FX");
            audioSource.PlayOneShot(hitSound);
            int facingDirection = transform.parent.position.x > collision.transform.position.x ? -1 : 1;
            damageable.Damage(damage, KBForce, new Vector2(KBAngle.x * facingDirection, KBAngle.y));
        }
    }
}
