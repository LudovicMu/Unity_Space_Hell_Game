using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    [SerializeField] IntVariable _enemyCount;
    [SerializeField] IntVariable _waveCount;
    [SerializeField] GameObject _enemyPrefab;

    private EnemyCount _enemyRecount;

    private GameObject[] _spawnpoints;
    private Transform _spawnpoint;

    private void Awake()
    {
        _spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
        _enemyRecount = GetComponent<EnemyCount>();
        _waveCount.value = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _enemyCount.value <= 0) {
            _waveCount.value += 1;
            for (int i = 0; i < 2 + _waveCount.value; i++)
            {
                _spawnpoint = _spawnpoints[Random.Range(0, 5)].transform;
                Instantiate(_enemyPrefab, _spawnpoint.position, _spawnpoint.rotation);
            }
            _enemyRecount.NewCount();
        }
    }
}
