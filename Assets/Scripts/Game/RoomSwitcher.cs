using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSwitcher : MonoBehaviour
{
    [SerializeField] private Fader _fader; 
    [SerializeField] GameStateMachine _gameStateMachine; // старайс€ придерживатьс€ одного код стайла. ≈сли начал у всех писать можификатор доступа то и пиши у всех. Ќа работу игры не скажетс€ но код выгл€дит при€тнее.
    // вот 1 из примеров код стайла https://avangarde-software.com/unity-coding-guidelines-basic-best-practices/
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
        var WaitForFadeEnd = new WaitForSeconds(_fader.FadeDuration); // ѕо сути можно было избавитьс€ от корутины если бы у Fader был ивент который бы дергалс€ по завершению

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

        yield return null; // зачем это тут?
    }
}
