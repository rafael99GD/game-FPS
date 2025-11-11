using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseState : BaseState
{
    private readonly int IsPlayerOnSight = Animator.StringToHash("IsPlayerOnSight");
    public override void Construct()
    {
        Debug.Log($"{gameObject.name} is entering Chase state");
        stateMotor.behaviour.SetAgentChaseSpeed();
        stateMotor.behaviour.SetAgentTarget(stateMotor.player.transform);
        stateMotor.anim?.SetBool(IsPlayerOnSight, stateMotor.isPlayerOnSight);
    }

    public override void Transition()
    {
        if (stateMotor.isPlayerOnSight) return;

        stateMotor.ChangeState(GetComponent<AIPatrolState>());


    }

    public override void UpdateState()
    {
        stateMotor.behaviour.SetMovingDestination();
    }

    public override void Destruct()
    {
        stateMotor.anim?.SetBool(IsPlayerOnSight, stateMotor.isPlayerOnSight);
        Debug.Log($"{gameObject.name} is exiting Chase state");
    }
}
