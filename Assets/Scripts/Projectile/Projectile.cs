using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform player;

    public float speed = 10f;
    private float projectileLife = 5f;
    private float timer;

    private Rigidbody2D rb;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        timer = 0f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim.SetTrigger("chase-1");
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate direction to player
            Vector2 direction = (player.position - transform.position).normalized;

            // Move towards player
            rb.velocity = direction * speed;
        }

        timer += Time.deltaTime;

        if (timer >= projectileLife)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall"))
        {
            Debug.Log("Hit PLayer");
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        anim.SetTrigger("exp-1");
        yield return new WaitForSeconds(projectileLife);
        Destroy(gameObject);
    }
}
