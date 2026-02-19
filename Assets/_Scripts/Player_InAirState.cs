using UnityEngine;

public class Player_InAirState : EntityState
{
    public Player_InAirState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(player.moveInput.x != 0)
        {
            player.SetVelocity(player.moveInput.x * player.moveSpeed * player.inAirMoveSpeedMutiplier, rb.linearVelocity.y);
        }
    }
}
