using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount);

    void Damage(float damageAmount, float KBForce, Vector2 KBAngle);
}
