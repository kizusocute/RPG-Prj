using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    private PlayerInputSet inputActions;

    private void Awake()
    {
        stateMachine = new StateMachine();
        inputActions = new PlayerInputSet();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Movement.performed += ctx => Debug.Log(ctx.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        //stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        
    }
}
