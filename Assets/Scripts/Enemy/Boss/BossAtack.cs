using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtack : MonoBehaviour
{
    public float damage;
    public Vector2 KBAngle;
    public float KBForce;

    public Transform position;
    public float checkRadius;
    public LayerMask damageableLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
