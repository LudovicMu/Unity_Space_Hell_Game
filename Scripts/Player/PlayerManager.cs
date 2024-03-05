using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _hurtDuration = 6f;

    private bool _hit;
    private float _hurtEndTime;

    private PlayerInput _input;
    private Rigidbody2D _rigidbody;

    public bool Hurt { get => _hit; }
    public bool IsHurtEnded { get { return Time.time >= _hurtEndTime; } }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void DoIdle()
    {
        Vector2 veloc = Vector2.zero;
        ApplyMovement(veloc);
    }

    public void DoMove()
    {
        Vector2 veloc = _input.ClampedMovement * _speed;
        ApplyMovement(veloc);
    }

    public void StartHurt()
    {
        _hurtEndTime = Time.time + _hurtDuration;
    }

    public void ExitHurt()
    {
        _hit = false;
    }

    private void ApplyMovement(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            _hit = true;
        }
    }
}
