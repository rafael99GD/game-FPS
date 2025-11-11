using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;

    [SerializeField]
    private float
        patrolSpeed,
        chaseSpeed,
        detectionRange,
        distanceToChangePoint;

    private NavMeshAgent _agent;
    private Transform _target;

    private int _currentPatrolPoint;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentPatrolPoint = 0;
        _agent.SetDestination(patrolPoints[_currentPatrolPoint].position);

        InitializeAgent();
    }

    private void InitializeAgent()
    {
        SetAgentPatrolSpeed();
    }

    public void ResetAgentPath()
    {
        _agent.ResetPath();
    }

    public void SetAgentPatrolSpeed()
    {
        SetAgentSpeed(patrolSpeed);
    }

    public void SetAgentChaseSpeed()
    {
        SetAgentSpeed(chaseSpeed);
    }

    public void SetAgentSpeed(float speed)
    {
        _agent.speed = speed;
    }

    public void SetAgentTarget( Transform target)
    {
        _target = target;
    }

    public void SetNextPatrolPoint()
    {
        if (patrolPoints.Length <= 0) return;

        SetAgentTarget(patrolPoints[(_currentPatrolPoint++)]);

        _currentPatrolPoint %= patrolPoints.Length;
    }

    public void SetMovingDestination()
    {
        if(_target == null) return;
        _agent.SetDestination(_target.position);
    }

    public void GoToNextPoint()
    {
        SetNextPatrolPoint();
        _agent.SetDestination(_target.position);
    }

    public bool CheckNextPoint()
    {
        return !_agent.pathPending && _agent.remainingDistance < distanceToChangePoint;
    }
}
