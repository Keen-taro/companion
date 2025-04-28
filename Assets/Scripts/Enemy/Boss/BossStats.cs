using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossStats")]
public class BossStats : ScriptableObject
{
    [Header("Boss Status")]
    public float maxHealthPoint;
    public float moveSpeed;
    public float intervalBetweenSteate;
    public float stoppingRange;
    
    [Header("Boss Attack Damage")]
    public float damageMeleeAttack;
    public float damageStationaryAttack;
    public float damageAirAttack;

    [Header("Boss Attack Range")]
    public float attackRange;
    public float chaseRange;
    public float teleportRange;

    [Header("Attack State")]
    public Vector2 knockbackAngleMelee;
    public float knockbackForceMelee;
    public Vector2 knockbackAngleAir;
    public float knockbackForceAir;
    public Vector2 knockbackAngleStationary;
    public float knockbackForceStationary;

    [Header("Boss Player Detection")]
    public float detectionPauseTime;
    public float playerDetectedWaitTime = 1;

    [Header("Charge State")]
    public float chaseTime;
    public float chaseSpeed;
    public float meleeDistance;

    [Header("Teleport Status")]
    public float teleportDistance;
}
