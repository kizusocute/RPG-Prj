using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator animator => player.animator;

    public EntityState(Player player ,StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() {
        animator.SetBool(animBoolName, true);
    }
    public virtual void LogicUpdate() { }
    public virtual void Exit() {
        animator.SetBool(animBoolName, false);
    }
}
