using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy
{
    private float _jumpForce;
    private int _damage = 10;

    protected override void Start()
    {
        base.Start();
        _jumpForce = Random.Range(2f, 3f);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Action()
    {
        StartCoroutine(JumpTowardsPlayer());
    }

    private IEnumerator JumpTowardsPlayer()
    {
        Vector3 lastPlayerPosition = _lastPlayerPosition;
        Vector3 lastDirectionToPlayer = lastPlayerPosition - transform.position;
        Vector3 jumpPoint = transform.position + (lastDirectionToPlayer.normalized * _jumpForce);

        _animator.SetBool("isMoving", true);

        if (Vector3.Distance(transform.position, jumpPoint) > Vector3.Distance(transform.position, lastDirectionToPlayer))
        {
            while (Vector3.Distance(transform.position, lastPlayerPosition) > 0.3f)
            {
                transform.LookAt(new Vector3(_player.transform.position.x, 0f, _player.transform.position.z));
                transform.position = Vector3.MoveTowards(transform.position, lastPlayerPosition, 3f * Time.deltaTime);

                yield return null;
            }
        }
        else
        {
            while (Vector3.Distance(transform.position, jumpPoint) > 0.3f)
            {
                transform.LookAt(new Vector3(_player.transform.position.x, 0f, _player.transform.position.z));
                transform.position = Vector3.MoveTowards(transform.position, jumpPoint, 3f * Time.deltaTime);

                yield return null;
            }
        }

        _myRigidbody.velocity = Vector3.zero;
        _animator.SetBool("isMoving", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _animator.SetTrigger("Attack");
            player.Attack(_damage);
        }
    }
}
