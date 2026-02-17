using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    private PlayerInputSet inputActions;
    public Vector2 moveInput { get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();
        inputActions = new PlayerInputSet();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
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
