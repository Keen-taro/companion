using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_OpenAndClose : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingMenu;
    public GameObject PauseMenu;
    public GameObject PlayerUI;
    public GameObject ObjectiveUI;
    public GameObject TutorialPanel;
    public GameObject StatusPanel;

    private bool inGame;
    private bool paused;
    public bool enableTutorial = true;

    private PlayerStateMachine players;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        players = player.GetComponent<PlayerStateMachine>();
    }

    public void StartGame()
    {
        MainMenu.SetActive(false);
        //PlayerUI.SetActive(true);
        ObjectiveUI.SetActive(true);
        TutorialPanel.SetActive(true);
        StatusPanel.SetActive(false);
        players.canMove = true;
        inGame = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            PauseMenu.SetActive(true);
            players.canMove = false;
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            ClosePauseMenu();
            paused = false;
        }

        if (enableTutorial && inGame)
        {
            TutorialPanel.SetActive(true);
        }
        else
        {
            TutorialPanel.SetActive(false);
        }
    }

    public void OpenStatus()
    {
        StatusPanel.SetActive(true);
    }

    public void ToggleTutorial()
    {
        enableTutorial = !enableTutorial;
    }

    public void ClosePauseMenu()
    {
        PauseMenu.SetActive(false);
        players.canMove = true;
    }

    public void OpenSetting()
    {
        if (PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(false);
        }

        SettingMenu.SetActive(true);
    }

    public void CloseSetting()
    {
        SettingMenu.SetActive(false);

        if (inGame)
        {
            PauseMenu.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        StatusPanel.SetActive(true);
    }
}
