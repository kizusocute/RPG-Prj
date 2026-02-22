using UnityEngine;

public class Player_BasicAttack : EntityState
{
    private float attackVelocityTimer;
    private int comboIndex = 1;
    private int FirstComboIndex = 1;
    private int maxCombo = 3;

    private float lastAttackTime;
    public Player_BasicAttack(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        //if (player.attackVelocity.Length != maxCombo)
        //{
        //    Debug.LogError("Attack velocity array length must be equal to max combo count.");
        //    maxCombo = player.attackVelocity.Length;
        //}
    }

    public override void Enter()
    {
        base.Enter();
        ResetComboIfNeed();
        attackVelocityTimer = player.attackVelocityTime;
        //GenerateAttackVelocity();
        animator.SetInteger("basicAttackIndex", comboIndex);
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

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        comboIndex++;
    }

    public void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocity.y);
        }
    }

    public void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        player.SetVelocity(player.facingDirection * attackVelocity.x, attackVelocity.y);
    }

    public void ResetComboIfNeed()
    {
        if (comboIndex > maxCombo || lastAttackTime + player.comboResetTime < Time.time)
            comboIndex = FirstComboIndex;
    }
}
