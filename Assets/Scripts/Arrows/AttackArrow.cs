using UnityEngine;
using UnityEngine.UI;

public class AttackArrow : MonoBehaviour
{
    [Header("Static Objects")]
    [SerializeField]
    private GameObject AttackLine;
    [SerializeField]
    private AttackColorSpriteDictionary ColorSprites;

    [Header("Arrow Settings")]
    [SerializeField]
    private AttackColor color = AttackColor.BLACK;
    [SerializeField]
    private ArrowMovementType movementType = ArrowMovementType.TRACKING;
    [SerializeField]
    private float chargeTime = 1f;

    [SerializeField]
    private Image chargeImage;
    [SerializeField]
    private SpriteRenderer flashSprite;

    private GameObject player;

    private float chargeProgress = 0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        switch (movementType)
        {
            case ArrowMovementType.STATIC:
                break;
            case ArrowMovementType.TRACKING:
                Tracking();
                break;
            default:
                break;
        }

        chargeProgress += Time.deltaTime / chargeTime;

        if (chargeProgress >= 0.9f)
        {
            flashSprite.enabled = true;
        }

        if (chargeProgress >= 1)
        {
            GameObject line = Instantiate(AttackLine, transform.position, transform.rotation);
            line.GetComponent<AttackLine>().AttackColor = color;
            Destroy(gameObject);
        }
        else
        {
            chargeImage.fillAmount = chargeProgress;
        }
    }

    private void Tracking()
    {
        Vector3 toPlayer = player.transform.position - transform.position;

        RotateTowards(toPlayer);
    }

    public void SetInstructions(ArrowMovementInstructions ins)
    {
        color = ins.ArrowColor;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = ColorSprites[color];

        chargeTime = ins.ChargeTime;

        switch(ins)
        {
            case ArrowMovementStatic a:
                movementType = ArrowMovementType.STATIC;
                RotateTowards(a.Direction);
                break;
            case ArrowMovementTracking a:
                movementType = ArrowMovementType.TRACKING;
                break;
            default:
                break;
        }
    }

    private void RotateTowards(Vector3 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 90);

        transform.rotation = rotation;
    }
}
