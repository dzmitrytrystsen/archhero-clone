using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdgradePanelController : MonoBehaviour
{
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private LevelCounter _levelManager;
    [SerializeField] private GameStateMachine _gameStateMachine;

    private void Awake()
    {
        _levelManager.OnLevelIncreased += ShowUpgradePanel;
    }

    private void ShowUpgradePanel()
    {
        _upgradePanel.SetActive(true);
        _pauseButton.SetActive(false);
        _gameStateMachine.SetGameOnPause();

        _upgradePanel.GetComponent<UpgradePanel>().OnUpgradeActivation += DisableUpgradePanel;
    }

    private void DisableUpgradePanel()
    {
        _upgradePanel.GetComponent<UpgradePanel>().OnUpgradeActivation -= DisableUpgradePanel;

        _upgradePanel.SetActive(false);
        _pauseButton.SetActive(true);
        _gameStateMachine.SetGameLive();
    }
}
