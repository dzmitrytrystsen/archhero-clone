using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameStateMachine gameStateMachine;


    private void Awake()
    {
        _player.OnHealthChanged += TryToEndGame;
        gameObject.SetActive(false);
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        _player.OnHealthChanged -= TryToEndGame;
    }

    private void ShowGameOverMenu()
    {
        gameObject.SetActive(true);
        gameStateMachine.SetGameOnPause();
    }

    private void TryToEndGame(int playerHealthLeft, GameObject player)
    {
        if (playerHealthLeft <= 0)
            ShowGameOverMenu();
    }
}
