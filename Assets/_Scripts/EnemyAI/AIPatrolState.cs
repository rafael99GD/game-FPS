using UnityEngine;

public class AIPatrolState : BaseState
{
    private readonly int IsIdleDone = Animator.StringToHash("IsIdleDone");
    private readonly int IsPlayerOnSight = Animator.StringToHash("IsPlayerOnSight");

    public override void Construct()
    {
        Debug.Log($"{gameObject.name} is entering Patrol state");
        stateMotor.behaviour.SetAgentPatrolSpeed();
        stateMotor.behaviour.GoToNextPoint();
        stateMotor.anim?.SetBool(IsIdleDone, stateMotor.isIdleDone);
        stateMotor.anim?.SetBool(IsPlayerOnSight, stateMotor.isPlayerOnSight);
    }

    public override void Transition()
    {
        if(stateMotor.isPlayerOnSight)
            stateMotor.ChangeState(GetComponent<AIChaseState>());

        if (stateMotor.behaviour.CheckNextPoint())
            stateMotor.ChangeState(GetComponent<AIIdleState>());

    }

    public override void UpdateState()
    {
        if (!stateMotor.behaviour.CheckNextPoint()) return;

        stateMotor.behaviour.GoToNextPoint();
    }

    public override void Destruct()
    {
        Debug.Log($"{gameObject.name} is exiting Patrol state");
    }
}
