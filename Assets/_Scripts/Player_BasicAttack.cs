using UnityEngine;

public class Player_BasicAttack : EntityState
{
    private float attackVelocityTimer;
    private int comboIndex = 1;
    private int FirstComboIndex = 1;
    private int maxCombo = 3;

    private int attackDirection;

    private bool comboAttackQueued;

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
        comboAttackQueued = false;
        ResetComboIfNeed();

        //Determine attack direction according to player input
        attackDirection = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDirection;
        //if(player.moveInput.x != 0)
        //    attackDirection = ((int)player.moveInput.x);
        //else
        //    attackDirection = player.facingDirection;

        ApplyAttackVelocity();
        animator.SetInteger("basicAttackIndex", comboIndex);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HandleAttackVelocity();

        if (player.inputActions.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        if (animTriggerCalled)
        {
            HandleStateExit();
        }
    }


    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        comboIndex++;
    }
    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            animator.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);
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
        attackVelocityTimer = player.attackVelocityTime;

        player.SetVelocity(attackDirection * attackVelocity.x, attackVelocity.y);
    }

    public void ResetComboIfNeed()
    {
        if (comboIndex > maxCombo || lastAttackTime + player.comboResetTime < Time.time)
            comboIndex = FirstComboIndex;
    }

    private void QueueNextAttack()
    {
        if(comboIndex < maxCombo)
            comboAttackQueued = true;
    }
}
