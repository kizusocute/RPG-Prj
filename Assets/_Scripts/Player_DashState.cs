using UnityEngine;

public class Player_DashState : EntityState
{
    private float originalGravityScale;
    private float dashDirection;

    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashTime;
        dashDirection = player.facingDirection;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CancelDashIfTouchedWall();
        player.SetVelocity(dashDirection * player.dashSpeed, 0f);

        if (stateTimer < 0f)
        {
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScale;
    }

    private void CancelDashIfTouchedWall()
    {
        if (player.wallDetected)
        {
            if (player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }
    }
}
