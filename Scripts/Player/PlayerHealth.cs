using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private IntVariable _playerStartHP;
    [SerializeField] private IntVariable _playerCurrentHP;

    public int Health { get => _playerCurrentHP.value; }

    private void Awake()
    {
        _playerCurrentHP.value = _playerStartHP.value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            _playerCurrentHP.value -= 1;
        }
    }
}
