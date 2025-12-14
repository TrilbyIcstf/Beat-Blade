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

    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 currentVelocity;
    private float currentSpeed = 0f;

    private Vector2 dashDirection;
    private bool dashing = false;
    private float dashCooldown = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    void FixedUpdate()
    {
        rb.linearVelocity = currentVelocity;
    }

    private bool HasMovementInput()
    {
        return input.magnitude > 0.1f;
    }
}
