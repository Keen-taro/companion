using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatsSO")]
public class StatsSO : ScriptableObject
{
    #region Variables

    [Header("Prefab")]
    public GameObject[] deathDebris;

    [Header("General Stats")]
    public float maxHealth;

    [Header("Patrol State")]
    public float speed;
    public float cliffDistance;

   [Header("Player Detection")]
    public float playerDetectDistance;
    public float detectionPauseTime;
    public float playerDetectedWaitTime = 1;

    [Header("Charge State")]
    public float chargeTime;
    public float chargeSpeed;
    public float meleeDistance;
    public float rangeDistance;

    [Header("Attack State")]
    public float damageAmount;
    public Vector2 knockbackAngle;
    public float knockbackForce;

    #endregion
}
