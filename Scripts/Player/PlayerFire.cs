using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private PlayerBullet _bulletPrefab;
    [SerializeField] private Transform _cannon;
    [SerializeField] private float _bulletSpeed = 2f;
    [SerializeField] private float _delayBetweenShots = 0.8f;

    private float _nextShotTime;

    private void Awake()
    {
        _nextShotTime = Time.time + _delayBetweenShots;
    }
    public void DoFire()
    {
        if (Time.time >= _nextShotTime)
        {
            FireBullet();
            _nextShotTime = Time.time + _delayBetweenShots;
        }
    }

    private void FireBullet()
    {
        PlayerBullet newBullet = Instantiate(_bulletPrefab, _cannon.position, _cannon.rotation);
        newBullet.Shoot(_bulletSpeed);
    }
}
