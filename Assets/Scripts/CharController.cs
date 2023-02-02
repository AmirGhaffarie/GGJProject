using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    [SerializeField] float speed = 7;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform groundCheckPoint;


    [SerializeField] float jumpForce;
    [SerializeField] float fallDampen;
    [SerializeField] float JumpBufferTime;
    [SerializeField] float OffPlatformBufferTime;
    [SerializeField] float deadZone = .1f;
    [SerializeField] float hangTime;

    private bool grounded = false;
    private bool jumping = false;
    private Rigidbody2D rb2d;
    private MainActionSet actionset;
    private BoxCollider2D col;

    private float offPlatformTime = 0;
    private float jumpPressedTime = 0;

    private Collider2D[] overlapGroundCheck = new Collider2D[2];

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        actionset = new MainActionSet();
        actionset.Enable();
    }

    void Update()
    {
        var deltaTime = Time.deltaTime;
        var input = actionset.Gameplay.Movement.ReadValue<Vector2>();
        rb2d.velocity = new Vector2(((Mathf.Abs(input.x) > deadZone)? input.x : 0 )* speed, rb2d.velocity.y);

        grounded = Physics2D.OverlapBoxNonAlloc(groundCheckPoint.position, new Vector2(.9f, .15f), 0, overlapGroundCheck, groundLayers.value) > 0 && rb2d.velocity.y == 0;

        if (grounded)
        {
            offPlatformTime = OffPlatformBufferTime;
            jumping = false;
        }

        if(rb2d.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(rb2d.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (actionset.Gameplay.Jump.WasPressedThisFrame())
            jumpPressedTime = JumpBufferTime;

        if (offPlatformTime > 0)
        {
            if (!jumping && jumpPressedTime > 0)
            {
                jumping = true;
                jumpPressedTime = 0;
                if (input.y < -deadZone)
                {
                    if (transform.position.y > -3.5f)
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
        if (jumping && actionset.Gameplay.Jump.WasReleasedThisFrame())
        {
            float newVelocity = rb2d.velocity.y > 0 ? rb2d.velocity.y * fallDampen : rb2d.velocity.y;
            rb2d.velocity = new Vector2(rb2d.velocity.x, newVelocity);
        }
        jumpPressedTime -= deltaTime;
        offPlatformTime -= deltaTime;
    }

    private void ResetCollider()
    {
        col.enabled = true;
    }

}
