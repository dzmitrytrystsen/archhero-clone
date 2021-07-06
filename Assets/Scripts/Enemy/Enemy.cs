using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public int ExperienceReward { get { return _experienceReward; } }

    [SerializeField] protected Animator _animator;
    [SerializeField] protected int _experienceReward = 15;

    protected Player _player;
    protected Vector3 _lastPlayerPosition;

    protected float _timeBeforeAction;
    protected bool _isReadyToAct;

    protected Rigidbody _myRigidbody;
    protected BoxCollider _myBoxCollider;

    protected virtual void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _myBoxCollider = GetComponent<BoxCollider>();
        _player = FindObjectOfType<Player>();

        StartCoroutine(GetPlayerPositionAndWait());
    }

    protected virtual void Update()
    {
        if (_isReadyToAct)
        {
            Action();
            StartCoroutine(GetPlayerPositionAndWait()); // Довольно запутанная логика. ЗАчем мешать корутину и апдейт? Лучше или то или то использовать
            _isReadyToAct = false;
        }
    }

    protected IEnumerator GetPlayerPositionAndWait()
    {
        _timeBeforeAction = Random.Range(1.5f, 2.5f);

        _lastPlayerPosition = _player.transform.position;

        yield return new WaitForSeconds(_timeBeforeAction);

        _isReadyToAct = true;

        StopCoroutine(GetPlayerPositionAndWait());
    }

    protected virtual void Action()
    {
        Debug.Log("Enemy is acting");
    }
}