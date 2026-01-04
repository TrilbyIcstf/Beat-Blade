using System.Collections;
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
    private SpriteRenderer sr;

    private float chargeProgress = 0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
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

    private IEnumerator FadeIn()
    {
        Color col = sr.color;
        col.a = 0;
        sr.color = col;

        float timer = 0;
        float goal = 0.5f;
        yield return new WaitUntil(() => {
            timer += Time.deltaTime;
            col.a = timer / goal;
            sr.color = col;

            return timer >= goal;
        });
    }

    private IEnumerator MoveIn(Direction dir)
    {
        Color col = sr.color;
        col.a = 0;
        sr.color = col;

        float rot = transform.rotation.eulerAngles.z;

        switch(dir)
        {
            case Direction.UP:
                break;
            case Direction.DOWN:
                rot += 180;
                break;
            case Direction.LEFT:
                rot += 90;
                break;
            case Direction.RIGHT:
                rot += 270;
                break;
        }

        float rotRad = rot * Mathf.Deg2Rad;
        Vector3 moveDir = new Vector3(Mathf.Cos(rotRad), Mathf.Sin(rotRad)).normalized;

        float timer = 0f;
        float goal = 0.5f;
        float dist = 0.5f;
        Vector3 basePos = transform.position;

        yield return new WaitUntil(() =>
        {
            timer += Time.deltaTime;
            col.a = timer / goal;
            sr.color = col;

            float tempDist = dist * (1 - (timer / goal));
            Vector3 displacement = tempDist * -moveDir.normalized;
            transform.position = basePos + displacement;

            return timer >= goal;
        });
    }

    public void SetInstructions(BeatMapArrow arrow)
    {
        color = arrow.Color;
        sr.sprite = ColorSprites[color];

        chargeTime = arrow.ChargeTime;

        switch (arrow)
        {
            case BeatMapArrowStatic a:
                SetInstructions(a);
                break;
            case BeatMapArrowTracking a:
                SetInstructions(a);
                break;
            default:
                break;
        }

        switch (arrow.SpawnMethod)
        {
            case ArrowSpawnMethod.FADE:
                StartCoroutine(FadeIn());
                break;
            case ArrowSpawnMethod.DOWNMOVINGFADE:
                StartCoroutine(MoveIn(Direction.DOWN));
                break;
            case ArrowSpawnMethod.UPMOVINGFADE:
                StartCoroutine(MoveIn(Direction.UP));
                break;
            case ArrowSpawnMethod.LEFTMOVINGFADE:
                StartCoroutine(MoveIn(Direction.LEFT));
                break;
            case ArrowSpawnMethod.RIGHTMOVINGFADE:
                StartCoroutine(MoveIn(Direction.RIGHT));
                break;
            default:
                break;
        }
    }

    private void SetInstructions(BeatMapArrowStatic arrow)
    {
        movementType = ArrowMovementType.STATIC;
        RotateTowards(arrow.Direction);
    }

    private void SetInstructions(BeatMapArrowTracking arrow)
    {
        movementType = ArrowMovementType.TRACKING;
        Tracking();
    }

    private void RotateTowards(Vector3 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = rotation;
    }
}
