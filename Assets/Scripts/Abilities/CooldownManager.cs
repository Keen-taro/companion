using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    // Dictionary to store last-used times for each ability
    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();

    // Public method to check if an ability is off cooldown
    public bool IsOffCooldown(string abilityName, float cooldownDuration)
    {
        // Check if ability exists and its cooldown has expired
        if (!cooldowns.ContainsKey(abilityName) || Time.time > cooldowns[abilityName])
        {
            return true;
        }
        return false;
    }

    // Public method to trigger an ability and set its cooldown
    public void UseAbility(string abilityName, float cooldownDuration)
    {
        cooldowns[abilityName] = Time.time + cooldownDuration;
    }
}
