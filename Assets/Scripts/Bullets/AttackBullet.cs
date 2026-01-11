using System.Collections;
using UnityEngine;

public class AttackBullet : MonoBehaviour
{
    [SerializeField]
    private AttackColorSpriteDictionary ColorSprites;

    [Header("Bullet Settings")]
    [SerializeField]
    private AttackColor color = AttackColor.BLACK;
    //[SerializeField]
    //private BulleteMovementType movementType = BulleteMovementType.TRACKING;
    [SerializeField]
    private float delayTime = 1f;
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private GameObject spriteObject;

    private GameObject player;
    private SpriteRenderer sr;

    [SerializeField]
    private Vector2 direction;
    private Vector2 position;

    private bool bulletActive = false;
    private float delayCounter = 0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = spriteObject.GetComponent<SpriteRenderer>();
        position = transform.position;
    }

    private void Update()
    {
        if (bulletActive)
        {
            position += direction * speed * Time.deltaTime;
            transform.position = position;

            if (!sr.isVisible)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            delayCounter += Time.deltaTime;
            if (delayCounter >= delayTime)
            {
                bulletActive = true;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
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

    public void SetInstructions(BeatMapBullet bullet)
    {
        color = bullet.Color;
        delayTime = bullet.Delay;
        speed = bullet.Speed;
        sr.sprite = ColorSprites[color];

        switch (bullet)
        {
            case BeatMapBulletStraight b:
                SetInstructions(b);
                break;
            case BeatMapBulletTracking b:
                SetInstructions(b);
                break;
        }

        switch (bullet.SpawnMethod)
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

    private void SetInstructions(BeatMapBulletStraight bullet)
    {
        direction = bullet.Direction.normalized;
        RotateTowards(direction);
    }

    private void SetInstructions(BeatMapBulletTracking bullet)
    {
        Vector3 toPlayer = player.transform.position - transform.position;
        RotateTowards(toPlayer);
    }

    private void RotateTowards(Vector3 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = rotation;
    }

    public AttackColor GetColor()
    {
        return color;
    }
}
