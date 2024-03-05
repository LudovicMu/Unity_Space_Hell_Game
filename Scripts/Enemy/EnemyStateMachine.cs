using UnityEngine;

public enum EnemyStates
{
    IDLE,
    MOVE,
    SHOOT,
    HURT,
    DEAD,
}

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyStates _currentState;

    private EnemyManager _enemyManager;
    private EnemyFire _enemyFire;
    private EnemyHealth _enemyHealth;

    public EnemyStates CurrentState { get => _currentState; private set => _currentState = value; }

    private void Awake()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _enemyFire = GetComponent<EnemyFire>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        OnStateUpdate(CurrentState);
    }
    private void FixedUpdate()
    {
        OnStateFixedUpdate(CurrentState);
    }

    private void OnStateEnter(EnemyStates state)
    {
        switch (state)
        {
            case EnemyStates.IDLE:
                OnEnterIdle();
                break;
            case EnemyStates.MOVE:
                OnEnterMove();
                break;
            case EnemyStates.SHOOT:
                OnEnterShoot();
                break;
            case EnemyStates.HURT:
                OnEnterHurt();
                break;
            case EnemyStates.DEAD:
                OnEnterDead();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(EnemyStates state)
    {
        switch (state)
        {
            case EnemyStates.IDLE:
                OnUpdateIdle();
                break;
            case EnemyStates.MOVE:
                OnUpdateMove();
                break;
            case EnemyStates.SHOOT:
                OnUpdateShoot();
                break;
            case EnemyStates.HURT:
                OnUpdateHurt();
                break;
            case EnemyStates.DEAD:
                OnUpdateDead();
                break;
        }
    }
    private void OnStateFixedUpdate(EnemyStates state)
    {
        switch (state)
        {
            case EnemyStates.IDLE:
                OnFixedUpdateIdle();
                break;
            case EnemyStates.MOVE:
                OnFixedUpdateMove();
                break;
            case EnemyStates.SHOOT:
                OnFixedUpdateShoot();
                break;
            case EnemyStates.HURT:
                OnFixedUpdateHurt();
                break;
            case EnemyStates.DEAD:
                OnFixedUpdateDead();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(EnemyStates state)
    {
        switch (state)
        {
            case EnemyStates.IDLE:
                OnExitIdle();
                break;
            case EnemyStates.MOVE:
                OnExitMove();
                break;
            case EnemyStates.SHOOT:
                OnExitShoot();
                break;
            case EnemyStates.HURT:
                OnExitHurt();
                break;
            case EnemyStates.DEAD:
                OnExitDead();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    private void TransitionToState(EnemyStates toState)
    {
        OnStateExit(CurrentState);
        CurrentState = toState;
        OnStateEnter(toState);
    }

    private void OnEnterIdle()
    {
        _enemyManager.StartIdle();
    }
    private void OnUpdateIdle()
    {
        if (_enemyManager.Hurt)
            TransitionToState(EnemyStates.HURT);
        else if (_enemyManager.IsNotOnScreen)
            TransitionToState(EnemyStates.MOVE);
        else if (_enemyManager.IsThinkEnded)
        {
            int Action = _enemyManager.Think();
            if (Action < 70)
                TransitionToState(EnemyStates.SHOOT);
            else
                TransitionToState(EnemyStates.MOVE);
        }
    }
    private void OnFixedUpdateIdle()
    {
    }
    private void OnExitIdle()
    {
    }

    private void OnEnterMove()
    {
        _enemyManager.StartMove();
    }
    private void OnUpdateMove()
    {
        if (_enemyManager.Hurt)
            TransitionToState(EnemyStates.HURT);
        else if (_enemyManager.IsMoveEnded)
        {
            TransitionToState(EnemyStates.IDLE);
        }
    }
    private void OnFixedUpdateMove()
    {
        _enemyManager.DoMove();
    }
    private void OnExitMove()
    {
    }

    private void OnEnterShoot()
    {
        _enemyFire.StartFire();
    }
    private void OnUpdateShoot()
    {
        if (_enemyManager.Hurt)
            TransitionToState(EnemyStates.HURT);
        else if (_enemyFire.IsShotEnded)
        {
            TransitionToState(EnemyStates.IDLE);
        }
    }
    private void OnFixedUpdateShoot()
    {
    }
    private void OnExitShoot()
    {
    }

    private void OnEnterHurt()
    {
        _enemyManager.StartHurt();
    }
    private void OnUpdateHurt()
    {
        if (_enemyHealth.Health <= 0)
            TransitionToState(EnemyStates.DEAD);
        else if (_enemyManager.IsHurtEnded)
        {
            TransitionToState(EnemyStates.IDLE);
        }
    }
    private void OnFixedUpdateHurt()
    {
    }
    private void OnExitHurt()
    {
        _enemyManager.ExitHurt();
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
