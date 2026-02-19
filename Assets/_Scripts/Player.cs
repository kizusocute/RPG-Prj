using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public PlayerInputSet inputActions {  get; private set; }

    public StateMachine stateMachine { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }

    [Header("Movement Details")]
    public float moveSpeed = 8f;
    public bool facingRight = true;
    public float jumpForce { get; private set; } = 12f;
    public float inAirMoveSpeedMutiplier { get; private set; } = .7f;

    [Header("Collisions Detect")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    public bool groundDetected {  get; private set; }

    public Vector2 moveInput { get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();
        inputActions = new PlayerInputSet();

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
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
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleGroundDetection();
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && facingRight == false) Flip();
        else if (xVelocity < 0 && facingRight) Flip();
    }
    
    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    public void HandleGroundDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}
