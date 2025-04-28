  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack singleton;

    public float damage;

    public float KBForce;
    public Vector2 KBAngle;

    public float[] damages;

    public Vector2[] KBAngles;
    public float[] KBForces;

    public GameObject attackCollider; // Reference to the attack collider
    private AttackColliderHandler attackColliderHandler;

    public float comboResetTime = 1f; // Time allowed to chain the next attack
    private int comboStep = 0;       // Tracks the current combo step
    private float lastAttackTime;    // Records the time of the last attack
    private float cd;

    public bool canAct = true;
    private bool cooldown = false;

    private const int maxComboSteps = 4;

    private Animator anim;

    private void Awake()
    {
        singleton = this;

        anim = GetComponent<Animator>();
        attackColliderHandler = attackCollider.GetComponent<AttackColliderHandler>();
    }

    private void Update()
    {
        // Check for attack input (e.g., left mouse button)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformComboAttack();
        }

        // Reset combo if time has passed
        if (Time.time - lastAttackTime > comboResetTime && comboStep != 0)
        {
            ResetCombo();
        }  
    }

    private void FixedUpdate()
    {
        if (cd > 0)
        {
            cd -= Time.deltaTime;
            //Debug.Log(cd);
        }

        if (cd <= 0)
        {
            cooldown = false;
            //Debug.Log(cooldown);
        }
    }

    void PerformComboAttack()
    {
        if (cooldown) return;
        if (!PlayerController.singleton.canMove) return;

        canAct = false;

        lastAttackTime = Time.time; // Update the last attack time

        // Increment and loop combo step
        comboStep = (comboStep % maxComboSteps) + 1;

        // Trigger the appropriate animation based on combo step
        anim.SetTrigger($"Attack{comboStep}");

        attackColliderHandler.SetAttackProperties(damages[comboStep - 1], KBAngles[comboStep - 1], KBForces[comboStep - 1]);

        if (comboStep == maxComboSteps)
        {
            ResetCombo();
            cooldown = true;
            cd = 2;
        }
    }

    void ResetCombo()
    {
        canAct = true;
        comboStep = 0;
    }
}
