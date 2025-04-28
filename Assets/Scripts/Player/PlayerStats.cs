using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Player Status")]
    public float maxSanityPoint;
    public float jumpForce;
    public float jumpCooldown;
    public bool isDie;

    [Header("Movement")]
    public float moveSpeed;
    public float move;
    public float runSpeed;

    [Header("Dash")]
    public float dashSpeed;
    public float dashDistance;
    public float dashingTime;
    public bool _isDashing;
    public bool _canDash;

    [Header("Steps Related")]
    public float footstepInterval = 0.5f;
    public float footstepRunInterval = 0.1f;

    [Header("Misc")]
    public float footstepTimer;
    public float gridSize;
    public float radius;

    [Header("Attack")]
    public float attackAvailableTime;
    public float attackComboTimer;

    [Header("Attack Status")]
    public float[] attackDamage;
    public float[] knockbackForce;
    public Vector2[] knockbackAngle;
}
