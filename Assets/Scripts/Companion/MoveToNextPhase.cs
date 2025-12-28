using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MoveToNextPhase : MonoBehaviour
{
    [SerializeField] PlayableDirector openingPhase;
    [SerializeField] GameObject ActivateGameObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (openingPhase != null)
            {
                openingPhase.Play();
            }

            if (ActivateGameObject != null)
            {
                ActivateGameObject.SetActive(true);
            }
        }
    }
}
