using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSwitcher : MonoBehaviour
{
    [SerializeField] private Fader _fader;
    [SerializeField] GameStateMachine _gameStateMachine;
    [SerializeField] EnemyCollector _enemyCollector;
    [SerializeField] private List<GameObject> _rooms = new List<GameObject>();

    private Player _player;
    private int _activeRoomIndex;
    private Vector3 _playerStartPosition;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _playerStartPosition = _player.transform.position;
        _activeRoomIndex = 0;
    }

    public void SwitchRoom()
    {
        StartCoroutine(SwitchRoomProcess());
    }

    private IEnumerator SwitchRoomProcess()
    {
        var WaitForFadeEnd = new WaitForSeconds(_fader.FadeDuration);

        _fader.FadeIn();

        yield return WaitForFadeEnd;

        if (_activeRoomIndex + 1 == _rooms.Count)
        {
            SaveSystem.SaveGameData(SaveSystem.LoadGameData().LevelsCompleted + 1);
            SceneManager.LoadScene(0);
        }
        else
        {
            _player.transform.position = _playerStartPosition;
            _rooms[_activeRoomIndex].SetActive(false);
            _activeRoomIndex += 1;
            _rooms[_activeRoomIndex].SetActive(true);
            _enemyCollector.CollectAllEnemiesAndSubscribe();
        }

        yield return null;
    }
}
