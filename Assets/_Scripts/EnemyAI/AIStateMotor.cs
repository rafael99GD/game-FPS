using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAIBehaviour), typeof(Animator))]
public class AIStateMotor : MonoBehaviour
{
    public Animator anim;
    public EnemyAIBehaviour behaviour;
    public PlayerMovement player;
    public bool isPlayerOnSight, isIdleDone;

    private BaseState _state;

    private void Awake()
    {
        behaviour = GetComponent<EnemyAIBehaviour>();
        anim = GetComponent<Animator>();
        _state = GetComponent<AIIdleState>();
    }

    private void Start()
    {
        _state.Construct();
    }

    private void Update()
    {
        //if(!GameManager.Instance.isGamePaused)
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        _state.Transition();
        _state.UpdateState();
    }

    public void ChangeState(BaseState newState)
    {
        _state.Destruct();
        _state = newState;
        _state.Construct();
    }
}
