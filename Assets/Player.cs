using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }
}
