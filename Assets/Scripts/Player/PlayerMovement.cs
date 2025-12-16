using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float MoveSpeed = 5f;
    [SerializeField]
    private float Acceleration = 10f;
    [SerializeField]
    private float Deceleration = 10f;
    [SerializeField]
    private float DashSpeed = 15f;
    [SerializeField]
    private float DashDeceleration = 50f;
    [SerializeField]
    private float DashCooldownTime = 0.75f;
    [SerializeField]
    private float DashIFrames = 0.3f;
    [SerializeField]
    private float DamageIFrames = 1f;

    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Vector2 input;
    private Vector2 currentVelocity;
    private float currentSpeed = 0f;

    private Vector2 dashDirection;
    private bool dashing = false;
    private float dashCooldown = 0;

    private float iFrames = 0;
    public float IFrames { get { return iFrames; } }

    private PlayerAttack attack;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        attack = GetComponent<PlayerAttack>();
    }

    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>().normalized;
    }

    public void OnDash(InputValue value)
    {
        if (!dashing && dashCooldown <= DashCooldownTime && HasMovementInput())
        {
            dashDirection = input;
            dashing = true;
            currentSpeed = DashSpeed;
            iFrames = DashIFrames;
        }
    }

    public void Update()
    {
        if (!dashing && dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }

        if (dashing)
        {
            currentSpeed = Mathf.Max(MoveSpeed, currentSpeed - (DashDeceleration * Time.deltaTime));
            currentVelocity = dashDirection * currentSpeed;

            if (currentSpeed <= MoveSpeed)
            {
                dashing = false;
                dashCooldown += DashCooldownTime;
            }
        }
        else if (HasMovementInput())
        {
            currentSpeed = Mathf.Min(MoveSpeed, currentSpeed + (Acceleration * Time.deltaTime));
            currentVelocity = input * currentSpeed;
        }
        else
        {
            currentSpeed = Mathf.Max(0, currentSpeed - (Deceleration * Time.deltaTime));
            currentVelocity = Vector2.zero;
        }

        if (iFrames > 0)
        {
            iFrames -= Time.deltaTime;

            if (iFrames <= 0)
            {
                attack.CheckCollision();
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = currentVelocity;
    }

    private bool HasMovementInput()
    {
        return input.magnitude > 0.1f;
    }

    public void TakeDamage()
    {
        iFrames = DamageIFrames;
    }
}
