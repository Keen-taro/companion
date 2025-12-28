using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverWhispLight : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Whisp"))
        {
            LightBehaviour whispLight = collision.GetComponent<LightBehaviour>();
            if (whispLight != null)
            {
                //whispLight.isRecovering = true;
            }
            else
            {
                Debug.Log("Not Found");
            }
        }
    }

    // Runs ONCE when the collider leaves the trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Whisp"))
        {
            LightBehaviour whispLight = collision.GetComponent<LightBehaviour>();

            if (whispLight != null)
            {
                //whispLight.isRecovering = false;
            }
        }
    }
}
