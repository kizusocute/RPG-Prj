using UnityEngine;

public class Player_FallState : EntityState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //if the player is falling and touches the ground, change to idle state
        //if (player.IsGrounded())
        //{
        //    stateMachine.ChangeState(player.idleState);
        //}
    }
}
