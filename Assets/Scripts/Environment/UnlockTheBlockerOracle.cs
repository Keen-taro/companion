using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTheBlockerOracle : MonoBehaviour
{
    [SerializeField] BoxCollider2D blockerCollider;
    [SerializeField] SpriteRenderer spriteBlocker;

    [SerializeField] private OracleManager stoneToUnlock;


    private void Update()
    {
        if(stoneToUnlock != null)
        {
            stoneToUnlock.GetComponent<OracleManager>();
        }

        if (stoneToUnlock.CheckIfComplete())
        {
            blockerCollider.enabled = false;
            spriteBlocker.enabled = true;
        }
    }
}
