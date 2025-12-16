using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float ParryWindowTime = 0.15f;
    [SerializeField]
    private float ParryCooldownTime = 0.5f;
    [SerializeField]
    private float ParryForgivenessTime = 0.1f;

    [SerializeField]
    private GameObject AttackLine;

    private SpriteRenderer sr;
    private CircleCollider2D cc;

    private AttackColor parryColor;

    private float parryWindow = 0f;
    private float parryCooldown = 0f;
    private float parryForgiveness = 0f;
    private AttackColor storedColor;
    private Collider2D storedCollider;

    private AimIndicator aim;
    private PlayerMovement move;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        aim = GetComponent<AimIndicator>();
        move = GetComponent<PlayerMovement>();
    }

    public void OnParryRed(InputValue value)
    {
        if (parryCooldown <= 0)
        {
            parryColor = AttackColor.RED;
            sr.color = Color.red;

            parryWindow = ParryWindowTime;
            parryCooldown = ParryCooldownTime;
        }
    }

    public void OnParryBlue(InputValue value)
    {
        if (parryCooldown <= 0)
        {
            parryColor = AttackColor.BLUE;
            sr.color = Color.blue;

            parryWindow = ParryWindowTime;
            parryCooldown = ParryCooldownTime;
        }
    }

    private void Update()
    {
        CheckParry();

        if (parryWindow > 0)
        {
            parryWindow -= Time.deltaTime;
            if (parryWindow <= 0)
            {
                ParryOff();
            }
        }

        if (parryCooldown > 0)
        {
            parryCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collision(collision);
    }

    public void CheckCollision()
    {
        Collider2D[] overlap = Physics2D.OverlapAreaAll(cc.bounds.min, cc.bounds.max);

        foreach (Collider2D collision in overlap)
        {
            Collision(collision);
        }
    }

    private void Collision(Collider2D collision)
    {
        if (collision.tag == "Attack Line")
        {
            parryForgiveness = ParryForgivenessTime;
            AttackLine al = collision.GetComponent<AttackLine>();
            storedColor = al.AttackColor;
            storedCollider = collision;
        }
    }

    private void ParryOff()
    {
        parryWindow = 0f;
        sr.color = Color.white;
    }

    private void CheckParry()
    {
        if (parryForgiveness > 0)
        {
            parryForgiveness -= Time.deltaTime;

            if (parryWindow > 0)
            {
                parryForgiveness = 0;
                if (parryColor == storedColor)
                {
                    ParrySuccess();
                }
                else
                {
                    ParryFail();
                }
            }
            else if (parryForgiveness <= 0)
            {
                ParryFail();
            }
        }
    }

    private void ParrySuccess()
    {
        //Debug.Log("Parry Success!!");
        Quaternion rotation = Quaternion.Euler(0, 0, aim.GetAngle());
        GameObject tempLine = Instantiate(AttackLine, gameObject.transform.position, rotation);

        PostParry();
    }

    private void ParryFail()
    {
        if (move.IFrames <= 0)
        {
            //Debug.Log("Parry Failure!!");
            move.TakeDamage();

            PostParry();
        }
    }

    private void PostParry()
    {
        //ParryOff();
        parryCooldown = 0f;
        if (storedCollider != null)
        {
            storedCollider.enabled = false;
        }
    }
}
