using UnityEngine;

public class BeatMapCreatorTempNote : MonoBehaviour
{
    [SerializeField] private NoteType type;

    private bool tracking = true;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        switch(type)
        {
            case NoteType.ARROWSTATIC:
                ArrowStatic();
                break;
            case NoteType.ARROWTRACKING:
                ArrowTracking();
                break;
            case NoteType.ARROWHORIZONTAL:
                ArrowHorizontal();
                break;
            case NoteType.ARROWVERTICAL:
                ArrowVertical();
                break;
        }
    }

    private void ArrowStatic()
    {
        if (tracking)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Vector2 direction = mousePos - transform.position;

            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            angle = angle / 15;
            angle = Mathf.Round(angle);
            angle = angle * 15;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void ArrowTracking()
    {
        Vector3 toPlayer = player.transform.position - transform.position;

        RotateTowards(toPlayer);
    }

    private void ArrowHorizontal()
    {
        if (player.transform.position.y > transform.position.y)
        {
            RotateTowards(Vector3.up);
        }
        else
        {
            RotateTowards(Vector3.down);
        }

        Vector2 playerHorizontal = new Vector2(player.transform.position.x, transform.position.y);
        transform.position = playerHorizontal;
    }

    private void ArrowVertical()
    {
        if (player.transform.position.x > transform.position.x)
        {
            RotateTowards(Vector3.right);
        }
        else
        {
            RotateTowards(Vector3.left);
        }

        Vector2 playerVertical = new Vector2(transform.position.x, player.transform.position.y);
        transform.position = playerVertical;
    }

    public void StopTracking()
    {
        tracking = false;
    }

    public void RotateTowards(Vector3 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = rotation;
    }

    public void SetType(NoteType type)
    {
        this.type = type;
    }
}
