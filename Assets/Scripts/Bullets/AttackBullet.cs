using System.Collections;
using UnityEngine;

public class AttackBullet : MonoBehaviour
{
    [SerializeField]
    private AttackColorSpriteDictionary ColorSprites;

    [Header("Bullet Settings")]
    [SerializeField]
    private AttackColor color = AttackColor.BLACK;
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private GameObject spriteObject;

    private GameObject player;
    private SpriteRenderer sr;

    [SerializeField]
    private Vector2 direction;
    private Vector2 position;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = spriteObject.GetComponent<SpriteRenderer>();
        position = transform.position;
    }

    private void Update()
    {
        position += direction * speed * Time.deltaTime;
        transform.position = position;

        if (!sr.isVisible)
        {
            Destroy(gameObject);
        }
    }

    public void SetInstructions(BeatMapBullet bullet)
    {
        color = bullet.Color;
        speed = bullet.Speed;
        direction = transform.right;
        sr.sprite = ColorSprites[color];
    }

    public AttackColor GetColor()
    {
        return color;
    }
}
