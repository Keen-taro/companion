using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    public float cooldownTime;
    public float activeTime;

    public enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    public AbilityState state = AbilityState.ready;

    private void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                // Automatically trigger ability when ready
                TriggerAbility();
                break;

            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;

            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    public void TriggerAbility()
    {
        if (state == AbilityState.ready)
        {
            ability.Activate(gameObject);
            state = AbilityState.active;
            activeTime = ability.activeTime;
        }
    }
}
