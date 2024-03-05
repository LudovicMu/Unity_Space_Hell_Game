using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 _movement;
    private Vector2 _normalizedMovement;
    private Vector2 _clampedMovement;

    private bool _shot;

    public Vector2 Movement { get => _movement; }
    public Vector2 NormalizedMovement { get => _normalizedMovement; }
    public Vector2 ClampedMovement { get => _clampedMovement; }
    public bool HasMovement { get => _movement.sqrMagnitude > 0f; }
    public bool Shot { get => _shot; }

    private void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _normalizedMovement = _movement.normalized;
        _clampedMovement = Vector2.ClampMagnitude(_movement, 1f);

        _shot = Input.GetButton("Jump");
    }
}
