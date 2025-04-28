using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleMemoryController : MonoBehaviour
{
    public static BubbleMemoryController singleton;

    public TextMeshProUGUI indicatorCollectedMemory;

    public GameObject[] bubbleMemory;
    public int collectedMemory;

    private void Awake()
    {
        singleton = this;
        indicatorCollectedMemory.text = (collectedMemory +  "  /  " + bubbleMemory.Length);
    }

    private void Update()
    {
        indicatorCollectedMemory.text = (collectedMemory + "  /  " + bubbleMemory.Length);

        if (collectedMemory == bubbleMemory.Length)
        {
            //Unlock Second lvl or second boss
        }
    }

    public void CollectedMemory()
    {
        collectedMemory++;
    }
}
