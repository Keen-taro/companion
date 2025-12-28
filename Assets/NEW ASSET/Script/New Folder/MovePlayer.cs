using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] Transform teleportTargetPosition;
    [SerializeField] GameObject parallax_Background;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FadeTransition(collision.gameObject);
        }
    }

    async void FadeTransition(GameObject player)
    {
        await ScreenFader.instance.FadeOut();

        parallax_Background.SetActive(false);
        player.transform.position = teleportTargetPosition.position;

        await ScreenFader.instance.FadeIn();
    }
}
