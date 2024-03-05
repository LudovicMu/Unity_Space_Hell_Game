using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float _scrollingSpeed = 10f;
    [SerializeField] private float _timeBeforeDestroy = 40f;

    private Rigidbody2D _rigibody;
    private float _timeDestroy;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _timeDestroy = Time.time + _timeBeforeDestroy;
    }

    private void FixedUpdate()
    {
        _rigibody.velocity = Vector2.down * _scrollingSpeed * Time.deltaTime;
        if (Time.time >= _timeDestroy)
        {
            Destroy(gameObject);
        }
    }
}
