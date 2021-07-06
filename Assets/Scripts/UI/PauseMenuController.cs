using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameStateMachine gameStateMachine;
    [SerializeField] Button _pauseButton;

    public void ShowPauseMenu()
    {
        _pauseButton.gameObject.SetActive(false);
        gameObject.SetActive(true);
        gameStateMachine.SetGameOnPause();
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueLevel()
    {
        gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        gameStateMachine.SetGameLive();
    }

    
}
