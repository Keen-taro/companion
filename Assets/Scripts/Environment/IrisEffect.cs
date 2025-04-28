using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IrisEffect : MonoBehaviour
{
    public Image maskImage; // Assign the circular mask
    public float transitionSpeed = 1f;

    void Start()
    {
        maskImage.fillAmount = 1f; // Start fully visible
    }

    public void TriggerIrisIn()
    {
        StartCoroutine(CloseIris());
    }

    private System.Collections.IEnumerator CloseIris()
    {
        while (maskImage.fillAmount > 0f)
        {
            maskImage.fillAmount -= Time.deltaTime * transitionSpeed;
            yield return null;
        }
        maskImage.fillAmount = 0f; // Fully closed
    }
}
