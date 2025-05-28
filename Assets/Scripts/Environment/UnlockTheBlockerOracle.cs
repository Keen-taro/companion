using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTheBlockerOracle : MonoBehaviour
{
    [SerializeField] BoxCollider2D blockerCollider;
    [SerializeField] SpriteRenderer spriteBlocker;

    [SerializeField] private OracleManager oracleToUnlock;


    private void Update()
    {
        if(oracleToUnlock != null)
        {
            oracleToUnlock.GetComponent<OracleManager>();
        }

        if (oracleToUnlock.CheckIfComplete())
        {
            blockerCollider.enabled = false;
            spriteBlocker.enabled = true;
        }
    }
}
