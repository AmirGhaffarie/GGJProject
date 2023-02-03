using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform groundCheckPoint;

    public float speed = 7;
    public float jumpForce;
    public float fallDampen;
    public int maxJumpCount;
    public float maxHangTime;

    public float JumpBufferTime;
    public float OffPlatformBufferTime;
    public float deadZone = .1f;

    private bool grounded = false;
    private Rigidbody2D rb2d;
    private MainActionSet actionset;
    private BoxCollider2D col;

    private float offPlatformTime = 0;
    private float jumpPressedTime = 0;
    private float remainingHangTime = 0;
    private float remainingJumps;

    private Collider2D[] overlapGroundCheck = new Collider2D[2];

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        remainingJumps = maxJumpCount;
        remainingHangTime = maxHangTime;

        actionset = new MainActionSet();
        actionset.Enable();
    }

    void Update()
    {
        var deltaTime = Time.deltaTime;
        var input = actionset.Gameplay.Movement.ReadValue<Vector2>();

        VerticalMovement(input);
        CheckIfGrounded();
        Jumping(input, deltaTime);

    }

    private void VerticalMovement(Vector2 input)
    {
        rb2d.velocity = new Vector2(((Mathf.Abs(input.x) > deadZone) ? input.x : 0) * speed, rb2d.velocity.y);
    }

    private void Jumping(Vector2 input, float deltaTime)
    {
        if (actionset.Gameplay.Jump.WasPressedThisFrame())
            jumpPressedTime = JumpBufferTime;

        if (offPlatformTime > 0 || remainingJumps < maxJumpCount)
        {
            if (jumpPressedTime > 0 && remainingJumps > 0)
            {
                remainingJumps--;
                jumpPressedTime = 0;
                if (input.y < -deadZone)
                {
                    if (transform.position.y > 0)
                    {
                        col.enabled = false;
                        Invoke(nameof(ResetCollider), 0.4f);
                    }
                }
                else
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                }
            }
        }

        if (!grounded && actionset.Gameplay.Jump.WasReleasedThisFrame())
        {
            float newVelocity = rb2d.velocity.y > 0 ? rb2d.velocity.y * fallDampen : rb2d.velocity.y;
            rb2d.velocity = new Vector2(rb2d.velocity.x, newVelocity);
        }
        jumpPressedTime -= deltaTime;
        offPlatformTime -= deltaTime;
    }

    private void CheckIfGrounded()
    {
        grounded = Physics2D.OverlapBoxNonAlloc(groundCheckPoint.position, new Vector2(.9f, .15f), 0, overlapGroundCheck, groundLayers.value) > 0 && rb2d.velocity.y == 0;

        if (grounded)
        {
            offPlatformTime = OffPlatformBufferTime;
            remainingJumps = maxJumpCount;
            remainingHangTime = maxHangTime;
        }

    }

    private void ResetCollider()
    {
        col.enabled = true;
    }

}
