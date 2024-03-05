using UnityEngine;

public enum PlayerStates
{
    IDLE,
    MOVE,
    MOVEANDSHOT,
    IDLEANDSHOT,
    HURT,
    DEAD,
}

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerStates _currentState;

    private PlayerInput _playerInput;
    private PlayerManager _playerManager;
    private PlayerFire _playerFire;
    private PlayerHealth _playerHealth;

    public PlayerStates CurrentState { get => _currentState; private set => _currentState = value; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerManager = GetComponent<PlayerManager>();
        _playerFire = GetComponent<PlayerFire>();
        _playerHealth = GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        OnStateUpdate(CurrentState);
    }
    private void FixedUpdate()
    {
        OnStateFixedUpdate(CurrentState);
    }

    private void OnStateEnter(PlayerStates state)
    {
        switch (state)
        {
            case PlayerStates.IDLE:
                OnEnterIdle();
                break;
            case PlayerStates.MOVE:
                OnEnterMove();
                break;
            case PlayerStates.MOVEANDSHOT:
                OnEnterMoveandshot();
                break;
            case PlayerStates.IDLEANDSHOT:
                OnEnterIdleandshot();
                break;
            case PlayerStates.HURT:
                OnEnterHurt();
                break;
            case PlayerStates.DEAD:
                OnEnterDead();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(PlayerStates state)
    {
        switch (state)
        {
            case PlayerStates.IDLE:
                OnUpdateIdle();
                break;
            case PlayerStates.MOVE:
                OnUpdateMove();
                break;
            case PlayerStates.MOVEANDSHOT:
                OnUpdateMoveandshot();
                break;
            case PlayerStates.IDLEANDSHOT:
                OnUpdateIdleandshot();
                break;
            case PlayerStates.HURT:
                OnUpdateHurt();
                break;
            case PlayerStates.DEAD:
                OnUpdateDead();
                break;
        }
    }
    private void OnStateFixedUpdate(PlayerStates state)
    {
        switch (state)
        {
            case PlayerStates.IDLE:
                OnFixedUpdateIdle();
                break;
            case PlayerStates.MOVE:
                OnFixedUpdateMove();
                break;
            case PlayerStates.MOVEANDSHOT:
                OnFixedUpdateMoveandshot();
                break;
            case PlayerStates.IDLEANDSHOT:
                OnFixedUpdateIdleandshot();
                break;
            case PlayerStates.HURT:
                OnFixedUpdateHurt();
                break;
            case PlayerStates.DEAD:
                OnFixedUpdateDead();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(PlayerStates state)
    {
        switch (state)
        {
            case PlayerStates.IDLE:
                OnExitIdle();
                break;
            case PlayerStates.MOVE:
                OnExitMove();
                break;
            case PlayerStates.MOVEANDSHOT:
                OnExitMoveandshot();
                break;
            case PlayerStates.IDLEANDSHOT:
                OnExitIdleandshot();
                break;
            case PlayerStates.HURT:
                OnExitHurt();
                break;
            case PlayerStates.DEAD:
                OnExitDead();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    private void TransitionToState(PlayerStates toState)
    {
        OnStateExit(CurrentState);
        CurrentState = toState;
        OnStateEnter(toState);
    }

    private void OnEnterIdle()
    {
    }
    private void OnUpdateIdle()
    {
        if (_playerManager.Hurt)
            TransitionToState(PlayerStates.HURT);
        if (_playerInput.HasMovement)
        {
            if (_playerInput.Shot)
                TransitionToState(PlayerStates.MOVEANDSHOT);
            else
                TransitionToState(PlayerStates.MOVE);
        }
        if (_playerInput.Shot)
            TransitionToState(PlayerStates.IDLEANDSHOT);
    }
    private void OnFixedUpdateIdle()
    {
        _playerManager.DoIdle();
    }
    private void OnExitIdle()
    {
    }

    private void OnEnterMove()
    {
    }
    private void OnUpdateMove()
    {
        if (_playerManager.Hurt)
            TransitionToState(PlayerStates.HURT);
        if (!_playerInput.HasMovement)
        {
            if (_playerInput.Shot)
                TransitionToState(PlayerStates.IDLEANDSHOT);
            else
                TransitionToState(PlayerStates.IDLE);
        }
        if (_playerInput.Shot)
            TransitionToState(PlayerStates.MOVEANDSHOT);
    }
    private void OnFixedUpdateMove()
    {
        _playerManager.DoMove();
    }
    private void OnExitMove()
    {
    }

    private void OnEnterMoveandshot()
    {
    }
    private void OnUpdateMoveandshot()
    {
        if (_playerManager.Hurt)
            TransitionToState(PlayerStates.HURT);
        if (!_playerInput.HasMovement)
        {
            if (_playerInput.Shot)
                TransitionToState(PlayerStates.IDLEANDSHOT);
            else
                TransitionToState(PlayerStates.IDLE);
        }
        if (!_playerInput.Shot)
            TransitionToState(PlayerStates.MOVE);
    }
    private void OnFixedUpdateMoveandshot()
    {
        _playerManager.DoMove();
        _playerFire.DoFire();
    }
    private void OnExitMoveandshot()
    {
    }

    private void OnEnterIdleandshot()
    {
    }
    private void OnUpdateIdleandshot()
    {
        if (_playerManager.Hurt)
            TransitionToState(PlayerStates.HURT);
        if (_playerInput.HasMovement)
        {
            if (_playerInput.Shot)
                TransitionToState(PlayerStates.MOVEANDSHOT);
            else
                TransitionToState(PlayerStates.MOVE);
        }
        if (!_playerInput.Shot)
            TransitionToState(PlayerStates.IDLE);
    }
    private void OnFixedUpdateIdleandshot()
    {
        _playerManager.DoIdle();
        _playerFire.DoFire();
    }
    private void OnExitIdleandshot()
    {
    }

    private void OnEnterHurt()
    {
        _playerManager.StartHurt();
    }
    private void OnUpdateHurt()
    {
        if (_playerHealth.Health <= 0)
        {
            TransitionToState(PlayerStates.DEAD);
        }
        else if (_playerManager.IsHurtEnded) 
        {
            if (_playerInput.HasMovement)
            {
                if (_playerInput.Shot)
                    TransitionToState(PlayerStates.MOVEANDSHOT);
                else
                    TransitionToState(PlayerStates.MOVE);
            }
            else
            {
                if (_playerInput.Shot)
                    TransitionToState(PlayerStates.IDLEANDSHOT);
                else
                    TransitionToState(PlayerStates.IDLE);
            }
        }
    }
    private void OnFixedUpdateHurt()
    {
        _playerManager.DoMove();
        if (_playerInput.Shot)
            _playerFire.DoFire();
    }
    private void OnExitHurt()
    {
        _playerManager.ExitHurt();
    }

    private void OnEnterDead()
    {
        Destroy(gameObject);
    }
    private void OnUpdateDead()
    {
    }
    private void OnFixedUpdateDead()
    {
    }
    private void OnExitDead()
    {
    }

}
