using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackground : MonoBehaviour
{
    [SerializeField] GameObject _backgroundPrefab;
    [SerializeField] private Transform _newSpawnPoint;

    bool _alreadySpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _alreadySpawned != true) {
            Instantiate(_backgroundPrefab, _newSpawnPoint.position, _newSpawnPoint.rotation);
            _alreadySpawned = true;
        }
    }
}
