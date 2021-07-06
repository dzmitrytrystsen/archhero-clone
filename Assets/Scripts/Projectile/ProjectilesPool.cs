using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPool : MonoBehaviour
{
    public List<GameObject> PooledProjectiles { get { return _pooledProjectiles; } }

    [SerializeField] private GeneralProjectile _projectilePrefab;

    private readonly int _amountToPool = 10;
    private readonly List<GameObject> _pooledProjectiles = new List<GameObject>(); // почему бы не хранить тут сразу ссылки на GeneralProjectile? Так было бы удобнее

    private void Awake()
    {
        GenenerateProjectilesPool(_projectilePrefab);
    }

    private void OnDestroy()
    {
        foreach (GameObject projectileGameObject in _pooledProjectiles)
        {
            GeneralProjectile projectile = projectileGameObject.GetComponent<GeneralProjectile>();
            projectile.OnReadyToReturnToThePool -= ReturnProjectileToThePool;
        }
    }

    public GameObject GetPooledProjectile()
    {
       return PoolFromList();
    }

    public void GenenerateProjectilesPool(GeneralProjectile projectilePrefab)
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject projectileGameObject = Instantiate<GameObject>(projectilePrefab.gameObject, transform.position, Quaternion.identity);
            projectileGameObject.transform.SetParent(transform);
            projectileGameObject.GetComponent<GeneralProjectile>().OnReadyToReturnToThePool += ReturnProjectileToThePool;
            projectileGameObject.SetActive(false);
            _pooledProjectiles.Add(projectileGameObject);
        }
    }

    private GameObject PoolFromList()
    {
        for (int i = 0; i < _pooledProjectiles.Count; i++)
        {
            if (!_pooledProjectiles[i].activeInHierarchy)
            {
                Rigidbody projectileRigidbody = _pooledProjectiles[i].GetComponent<Rigidbody>();
                projectileRigidbody.velocity = Vector3.zero;
                projectileRigidbody.angularVelocity = Vector3.zero;
                _pooledProjectiles[i].transform.rotation = Quaternion.Euler(Vector3.zero);

                return _pooledProjectiles[i];
            }
        }

        return null;
    }

    private void ReturnProjectileToThePool(GameObject projectile)
    {
        projectile.SetActive(false);
    }
}
