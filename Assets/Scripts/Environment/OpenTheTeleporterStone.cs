using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheTeleporterStone : MonoBehaviour
{
    public GameObject teleporter;
    [SerializeField] private StonePlace stoneToUnlock;

    // Update is called once per frame
    void Update()
    {
        stoneToUnlock.GetComponent<StonePlace>();

        if (stoneToUnlock.CheckIfComplete())
        {
            teleporter.SetActive(true);
        }
    }
}
