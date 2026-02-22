using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Rigidbody2D rb => player.rb;
    protected Animator animator => player.animator;
    protected float stateTimer;

    protected bool animTriggerCalled;

    public EntityState(Player player ,StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() {
        animTriggerCalled = false;
        animator.SetBool(animBoolName, true);
    }
    public virtual void LogicUpdate() {
        stateTimer -= Time.deltaTime;
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

        if(player.inputActions.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }
    public virtual void Exit() {
        animator.SetBool(animBoolName, false);
    }

    public void CallAnimTrigger()
    {
        animTriggerCalled = true;
    }

    public bool CanDash()
    {
        if(stateMachine.currentState == player.dashState)
            return false;
        else if(player.wallDetected)
            return false;
        return true;
    }   
}
