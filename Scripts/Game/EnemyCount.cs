using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private IntVariable _enemyCount;

    private void Awake()
    {
        _enemyCount.value = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    public void NewCount()
    {
        _enemyCount.value = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
