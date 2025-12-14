using UnityEngine;

public class AttackArrow : MonoBehaviour
{
    [SerializeField]
    private AttackColor color = AttackColor.BLACK;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 toPlayer = player.transform.position - transform.position;

        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 90);

        transform.rotation = rotation;
    }
}
