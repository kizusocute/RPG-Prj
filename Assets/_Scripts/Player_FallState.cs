using UnityEngine;

public class Player_FallState : Player_InAirState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
