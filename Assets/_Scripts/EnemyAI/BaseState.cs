using UnityEngine;

[RequireComponent(typeof(AIStateMotor))]
public abstract class BaseState : MonoBehaviour, ITriggerTarget, ITriggerExitable
{
    [SerializeField] protected AIStateMotor stateMotor;

    private void Awake()
    {
        stateMotor = GetComponent<AIStateMotor>();
    }

    public virtual void Construct() { }

    public virtual void Destruct() { }

    public virtual void Transition() { }

    public virtual void UpdateState() { }

    public void HitByPlayer(PlayerMovement player)
    {
        stateMotor.player = player;
        stateMotor.isPlayerOnSight = true;
    }

    public void ExitedByPlayer()
    {
        stateMotor.isPlayerOnSight = false;
    }
}
