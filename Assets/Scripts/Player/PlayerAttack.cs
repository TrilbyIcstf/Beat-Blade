using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float ParryWindowTime = 0.15f;
    [SerializeField]
    private float ParryCooldownTime = 0.75f;

    [SerializeField]
    private GameObject AttackLine;

    private SpriteRenderer sr;

    private AttackColor parryColor;

    private float parryWindow = 0f;
    private float parryCooldown = 0f;

    private AimIndicator aim;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        aim = GetComponent<AimIndicator>();
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

    private void Update()
    {
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
        if (collision.tag == "Attack Line")
        {
            AttackLine al = collision.GetComponent<AttackLine>();

            if (parryWindow > 0 && parryColor == al.AttackColor)
            {
                Debug.Log("Parry Success!!");
                Quaternion rotation = Quaternion.Euler(0, 0, aim.GetAngle());
                GameObject tempLine = Instantiate(AttackLine, gameObject.transform.position, rotation);

                ParryOff();
                parryCooldown = 0f;
            } 
            else
            {
                Debug.Log("Parry Fail!!");
            }
        }
    }

    private void ParryOff()
    {
        parryWindow = 0f;
        sr.color = Color.white;
    }
}
