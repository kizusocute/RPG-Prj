using UnityEditor.Purchasing;
using UnityEngine;

public class Player_JumpAttackState : EntityState
{
    private bool groundTouched;

    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        groundTouched = false;
        player.SetVelocity(player.facingDirection * player.jumpAttackVelocity.x, player.jumpAttackVelocity.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.groundDetected && groundTouched == false)
        {
            groundTouched = true;
            animator.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocity.y);
        }

        if(animTriggerCalled && player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
