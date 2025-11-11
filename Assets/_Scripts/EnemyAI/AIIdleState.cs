using UnityEngine;


public class AIIdleState : BaseState
{
    [SerializeField] private float idleCooldown;

    private readonly int IsIdleDone = Animator.StringToHash("IsIdleDone");

    private float _idleTimer;

    public override void Construct()
    {
        Debug.Log($"{gameObject.name} is entering Idle state");
        stateMotor.behaviour.ResetAgentPath();
        stateMotor.isIdleDone = false;
        stateMotor.anim?.SetBool(IsIdleDone, stateMotor.isIdleDone);
        _idleTimer = idleCooldown;
    }

    public override void Transition()
    {
        if(stateMotor.isPlayerOnSight) 
        {
            stateMotor.ChangeState(GetComponent<AIChaseState>());
        }

        if(stateMotor.isIdleDone)
        {
            stateMotor.ChangeState(GetComponent<AIPatrolState>());
        }
    }

    public override void UpdateState()
    {
        _idleTimer -= Time.deltaTime;
        stateMotor.isIdleDone = _idleTimer <= 0;
    }

    public override void Destruct()
    {
        stateMotor.isIdleDone = true;
        stateMotor.anim?.SetBool(IsIdleDone, stateMotor.isIdleDone);
        Debug.Log($"{gameObject.name} is exiting Idle state");
    }
}
