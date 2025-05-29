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

    public GameObject statusPanel;

    private void Awake()
    {
        singleton = this;
        collectedMemory = 0;
        indicatorCollectedMemory.text = (collectedMemory +  "  /  " + bubbleMemory.Length);
    }

    private void Update()
    {
        indicatorCollectedMemory.text = (collectedMemory + "  /  " + bubbleMemory.Length);

        if (collectedMemory == bubbleMemory.Length)
        {
            statusPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void CollectedMemory()
    {
        collectedMemory++;
    }
}
