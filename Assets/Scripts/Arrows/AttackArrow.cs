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
    private GameObject spriteObject;
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
        sr = spriteObject.GetComponent<SpriteRenderer>();
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
            case ArrowMovementType.HORIZONTAL:
                Horizontal();
                break;
            case ArrowMovementType.VERTICAL:
                Vertical();
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

    private void FacePlayerHorizontal()
    {
        if (player.transform.position.y > transform.position.y)
        {
            RotateTowards(Vector3.up);
        }
        else
        {
            RotateTowards(Vector3.down);
        }
    }

    private void FacePlayerVertical()
    {
        if (player.transform.position.x > transform.position.x)
        {
            RotateTowards(Vector3.right);
        }
        else
        {
            RotateTowards(Vector3.left);
        }
    }

    private void Horizontal()
    {
        if (chargeProgress >= 0.9f) { return; }
        Vector2 playerHorizontal = new Vector2(player.transform.position.x, transform.position.y);
        transform.position = playerHorizontal;
    }

    private void Vertical()
    {
        if (chargeProgress >= 0.9f) { return; }
        Vector2 playerVertical = new Vector2(transform.position.x, player.transform.position.y);
        transform.position = playerVertical;
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

        switch (dir)
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

        yield return new WaitUntil(() =>
        {
            timer += Time.deltaTime;
            col.a = timer / goal;
            sr.color = col;

            float tempDist = dist * (1 - (timer / goal));
            Vector3 displacement = tempDist * -moveDir.normalized;
            spriteObject.transform.position = transform.position + displacement;

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
            case BeatMapArrowHorizontal a:
                SetInstructions(a);
                break;
            case BeatMapArrowVertical a:
                SetInstructions(a);
                break;
            default:
                break;
        }

        switch (arrow.SpawnMethod)
        {
            case NoteSpawnMethod.FADE:
                StartCoroutine(FadeIn());
                break;
            case NoteSpawnMethod.DOWNMOVINGFADE:
                StartCoroutine(MoveIn(Direction.DOWN));
                break;
            case NoteSpawnMethod.UPMOVINGFADE:
                StartCoroutine(MoveIn(Direction.UP));
                break;
            case NoteSpawnMethod.LEFTMOVINGFADE:
                StartCoroutine(MoveIn(Direction.LEFT));
                break;
            case NoteSpawnMethod.RIGHTMOVINGFADE:
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

    private void SetInstructions(BeatMapArrowHorizontal arrow)
    {
        movementType = ArrowMovementType.HORIZONTAL;
        FacePlayerHorizontal();
    }

    private void SetInstructions(BeatMapArrowVertical arrow)
    {
        movementType = ArrowMovementType.VERTICAL;
        FacePlayerVertical();
    }

    private void RotateTowards(Vector3 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = rotation;
    }
}
