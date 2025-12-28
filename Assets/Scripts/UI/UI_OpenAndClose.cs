using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class UI_OpenAndClose : MonoBehaviour
{
    private bool paused;
    public bool enableTutorial = true;

    private PlayerControllerSimplified players;
    public PlayableDirector director;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        players = player.GetComponent<PlayerControllerSimplified>();
    }

    public void StartGame()
    {

        if (director != null)
        {
            director.Play();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
