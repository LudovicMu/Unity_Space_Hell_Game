using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private IntVariable _enemyType1StartHP;
    [SerializeField] private IntVariable _enemyCount;
    private int _enemyCurrentHP;

    public int Health { get => _enemyCurrentHP; }

    private void Awake()
    {
        _enemyCurrentHP = _enemyType1StartHP.value;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            _enemyCurrentHP -= 1;
        }
    }

    private void OnDestroy()
    {
        _enemyCount.value -= 1;
    }
}
