using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public int Level
    {
        get { return _playerStats.Level; }
        set
        {
            _playerStats.Level = value;
        }
    }

    public int MaxHealth { get { return _playerStats.MaxHealth; } }

    public delegate void MaxHealthIncreaseAction(int newMaxHealthValue);
    public event MaxHealthIncreaseAction OnHealthIncreased;

    public event MaxHealthIncreaseAction OnHealed; // events ?????? ????? ? ????? ????? ??????

    [SerializeField] public Animator Animator;
    public Joystick Joystick { get { return _joystick; } }
    public Enemy NearestEnemy { get { return _nearestEnemy; } }

    [SerializeField] private ProjectilesPool _projectilesPool;
    [SerializeField] GameObject _targetPointerPrefab;
    [SerializeField] private PlayerStats _playerStats;

    private GameObject _targetPointer;
    private AudioSource _audioSource;

    private Joystick _joystick;
    private CharacterController _characterController;

    private Vector3 _moveDirection;
    private Enemy[] _enemies;
    private Enemy _nearestEnemy; // ????? ??? ??? ?????????? ???? ??? ???????????? ?????? ? ????? ?????. ?????? ????? ??????? ????? DetectNearestEnemy ????????? ????? ????? ??? ?????. ??????? ????????? ??????? ???
    private bool _ifShooted;

    private Dictionary<Type, IPlayerBehavior> _behaviorsMap;
    private IPlayerBehavior _currentBehavior;

    private void Awake()
    {
        _health = _playerStats.MaxHealth;

        _joystick = FindObjectOfType<Joystick>();
        _audioSource = GetComponent<AudioSource>();
        _characterController = FindObjectOfType<CharacterController>();
        _joystick.OnDragHappend += SetBehaviorMoving;
        _joystick.OnDragEnded += SetBehaviorIdle;

        GenerateTargetPointer();
    }

    private void Start()
    {
        InitBehaviors();
        SetBehaviorByDefault();
    }

    private void OnDestroy()
    {
        _joystick.OnDragHappend -= SetBehaviorMoving;
        _joystick.OnDragEnded -= SetBehaviorIdle;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_currentBehavior != null)
            _currentBehavior.FixedUpdate(this);

        if (transform.position.y > 0.25f)
        {
            Vector3 groundedPosition = new Vector3(transform.position.x, 0.25f, transform.position.z);
            transform.position = groundedPosition;
        }
    }

    private void Update()
    {
        if (_currentBehavior != null)
            _currentBehavior.Update(this);
    }

    public void HideTargetPointer()
    {
        _targetPointer.SetActive(false);
    }

    public void Heal()
    {
        _health = _playerStats.MaxHealth;
        OnHealed?.Invoke(_health);
    }

    public void IncreaseMaxHealth(int healthAmountToIncrease)
    {
        _playerStats.MaxHealth += healthAmountToIncrease;
        OnHealthIncreased?.Invoke(_playerStats.MaxHealth);
    }

    public void Move()
    {
        // Move
        float horizontalAxis = _joystick.Horizontal;
        float verticalAxis = _joystick.Vertical;

        _moveDirection = new Vector3(horizontalAxis, 0f, verticalAxis);

        Vector3 movement = _playerStats.Speed * Time.deltaTime * _moveDirection.normalized;

        _characterController.Move(movement);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_moveDirection);
        }
    }

    public void ShootOnIdle()
    {
        if (IsEnemyAlive())
        {
            DetectNearestEnemy();
            ShowTargetPointer();

            // Look at nearest enemy
            transform.LookAt(new Vector3(_nearestEnemy.transform.position.x, 0f, _nearestEnemy.transform.position.z));

            if (!_ifShooted)
            {
                Animator.SetTrigger("Shoot");
                float force = 7f;

                GameObject currentProjectile = _projectilesPool.GetPooledProjectile();
                currentProjectile.transform.position = transform.position;
                currentProjectile.GetComponent<GeneralProjectile>().SetDamage = _playerStats.Damage;
                currentProjectile.SetActive(true);
                currentProjectile.GetComponent<GeneralProjectile>().OnReadyToReturnToThePool += AskToPlayProjectileImpactSound;
                currentProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
                AudioEffects.Instance.PlaySoundEffect(NameOfSoundEffect.ProjectileEffect, _audioSource);

                StartCoroutine(ShootAndWait());
                _ifShooted = true;
            }
        }
        else
        {
            SetBehaviorIdle();
        }
    }

    private IEnumerator ShootAndWait()
    {
        yield return new WaitForSeconds(_playerStats.FireSpeed);

        _ifShooted = !_ifShooted;
    }

    public void ShowTargetPointer()
    {
        _targetPointer.transform.position = _nearestEnemy.transform.position;
        _targetPointer.SetActive(true);
    }

    private void GenerateTargetPointer()
    {
        _targetPointer = Instantiate(_targetPointerPrefab, transform.position + Vector3.up, Quaternion.identity);
        _targetPointer.transform.SetParent(transform);
        _targetPointer.SetActive(false);
    }

    private void CollectAllEnemies()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    private void DetectNearestEnemy()
    {
        _nearestEnemy = _enemies[0];

        float distance = Vector3.Distance(transform.position, _enemies[0].transform.position);

        for (int i = 0; i < _enemies.Length; i++)
        {
            float tempDistance = Vector3.Distance(transform.position, _enemies[i].transform.position);

            if (tempDistance < distance)
                _nearestEnemy = _enemies[i];
        }
    }

    private bool IsEnemyAlive()
    {
        CollectAllEnemies();

        return _enemies.Length > 0;
    }

    private void AskToPlayProjectileImpactSound(GameObject projectile)
    {
        AudioEffects.Instance.PlaySoundEffect(NameOfSoundEffect.ProjectileImpact, _audioSource);
    }

    private void InitBehaviors()
    {
        _behaviorsMap = new Dictionary<Type, IPlayerBehavior>();

        _behaviorsMap[typeof(PlayerBehaviorIdle)] = new PlayerBehaviorIdle();
        _behaviorsMap[typeof(PlayerBehaviorShooting)] = new PlayerBehaviorShooting();
        _behaviorsMap[typeof(PlayerBehaviorMoving)] = new PlayerBehaviorMoving();

    }

    private void SetBehavior(IPlayerBehavior newBehavior)
    {
        if (_currentBehavior != null)
            _currentBehavior.Exit(this);

        _currentBehavior = newBehavior;
        _currentBehavior.Enter(this);
    }

    private void SetBehaviorByDefault()
    {
        SetBehaviorIdle();
    }

    private IPlayerBehavior GetBehavior<T>() where T : IPlayerBehavior
    {
        var type = typeof(T);
        return _behaviorsMap[type];
     }

    private void SetBehaviorIdle()
    {
        if (IsEnemyAlive())
            SetBehaviorShooting();
        else
        {
            var behavior = GetBehavior<PlayerBehaviorIdle>();
            SetBehavior(behavior);
        }
    }

    private void SetBehaviorShooting()
    {
        var behavior = GetBehavior<PlayerBehaviorShooting>();
        SetBehavior(behavior);
    }

    private void SetBehaviorMoving()
    {
        var behavior = GetBehavior<PlayerBehaviorMoving>();
        SetBehavior(behavior);
    }
}