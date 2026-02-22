using UnityEngine;

public class Player_BasicAttack : EntityState
{
    private float attackVelocityTimer;
    public Player_BasicAttack(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        attackVelocityTimer = player.attackVelocityTime;
        //GenerateAttackVelocity();
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HandleAttackVelocity();
        if (animTriggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocity.y);
        }
    }

    public void GenerateAttackVelocity()
    {
        player.SetVelocity(player.facingDirection * player.attackVelocity.x, player.attackVelocity.y);
    }
}
