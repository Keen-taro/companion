using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTheBlocker : MonoBehaviour
{
    [SerializeField] BoxCollider2D blockerCollider;
    [SerializeField] SpriteRenderer spriteBlocker;

    [SerializeField] private StonePlace stoneToUnlock;


    private void Update()
    {
        stoneToUnlock.GetComponent<StonePlace>();

        if (stoneToUnlock.CheckIfComplete())
        {
            blockerCollider.enabled = false;
            spriteBlocker.enabled = true;
        }
    }
}
