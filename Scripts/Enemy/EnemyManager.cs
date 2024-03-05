using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _thinkingDuration = 0.6f;
    [SerializeField] private float _hurtDuration = 0.2f;

    private Rigidbody2D _rigidbody;
    private Transform _transform;

    private GameObject[] _waypoints;
    private Transform _waypoint;

    private bool _needToMove;
    private bool _hit;
    private float _thinkEndTime;
    private float _moveEndTime;
    private float _hurtEndTime;

    public bool IsHurtEnded { get { return Time.time >= _hurtEndTime; } }
    public bool IsThinkEnded { get { return Time.time >= _thinkEndTime; } }
    public bool IsMoveEnded { get { return Time.time >= _moveEndTime; } }
    public bool Hurt { get => _hit; }
    public bool IsNotOnScreen { get => _needToMove; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        _needToMove = true;
    }
    public int Think()
    {
        int randAction = Random.Range(0, 100);
        return randAction;
    }

    public void StartIdle()
    {
        Vector2 veloc = Vector2.zero;
        ApplyMovement(veloc);
        _thinkEndTime = Time.time + _thinkingDuration;
    }
    public void StartMove()
    {
        _waypoint = _waypoints[Random.Range(0, 5)].transform;
        _moveEndTime = Time.time + Random.Range(1, 3);
    }

    public void DoMove()
    {
        Vector2 veloc = new Vector2(_waypoint.position.x - _transform.position.x, _waypoint.position.y - _transform.position.y) * _speed;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Vector2 veloc = Vector2.zero;
            ApplyMovement(veloc);
            _hit = true;
        }
    }

    private void OnBecameInvisible()
    {
        _needToMove = true;
    }

    private void OnBecameVisible()
    {
        _needToMove = false;
    }
}
