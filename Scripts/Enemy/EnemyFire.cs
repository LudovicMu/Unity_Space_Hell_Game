using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private EnemyBullet _bulletPrefab;
    [SerializeField] private Transform _cannon;
    [SerializeField] private float _bulletSpeed = 0.5f;
    [SerializeField] private float _delayBetweenShots = 3f;

    private float _nextShotTime;
    public bool IsShotEnded { get { return Time.time >= _nextShotTime; } }

    public void StartFire()
    {
        FireBullet();
        _nextShotTime = Time.time + _delayBetweenShots;
    }

    private void FireBullet()
    {
        for (int i = 0; i < Random.Range(1, 8); i++)
        {
            EnemyBullet newBullet = Instantiate(_bulletPrefab, _cannon.position, _cannon.rotation);
            newBullet.Shoot(_bulletSpeed);
        }
    }
}
