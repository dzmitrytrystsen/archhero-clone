using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _homePage;
    [SerializeField] private GameObject _levelsPage;

    public void Start()
    {
        _levelsPage.SetActive(false);
    }

    public void MoveToFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void MoveToLevelsPage()
    {
        _levelsPage.SetActive(true);
        _homePage.SetActive(false);
    }

    public void MoveToHomePage()
    {
        _homePage.SetActive(true);
        _levelsPage.SetActive(false);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}