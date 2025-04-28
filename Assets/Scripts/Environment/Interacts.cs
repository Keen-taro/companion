using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacts : MonoBehaviour
{
    //Interact button
    public GameObject interactUI;
    public GameObject hints_panel;
    public bool insideAreaInteract;

    private void Update()
    {
        intereactWithPlayer();
    }

    public void intereactWithPlayer()
    {
        if (insideAreaInteract && Input.GetKeyDown(KeyCode.E))
        {
            interactUI.SetActive(false);
            hints_panel.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.SetActive(true);
            insideAreaInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.SetActive(false);
            insideAreaInteract = false;

            if (!insideAreaInteract)
            {
                hints_panel.SetActive(false);
            }
        }
    }
}
