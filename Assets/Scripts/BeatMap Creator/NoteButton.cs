using UnityEngine;

public class NoteButton : MonoBehaviour
{
    public NoteType noteType;

    float timer = 0;
    Vector2 basePos;

    private void Awake()
    {
        basePos = transform.position;
    }

    private void OnMouseDown()
    {
        GameObject creatorObject = GameObject.FindGameObjectWithTag("BeatMap Creator");
        BeatMapCreator creator = creatorObject.GetComponent<BeatMapCreator>();
        creator.StartPlacing(noteType);
    }

    private void FixedUpdate()
    {
        switch(noteType)
        {
            case NoteType.ARROWSTATIC:
                break;
            case NoteType.ARROWTRACKING:
                {
                    Vector3 rotate = new Vector3(0f, 0f, 6f);
                    gameObject.transform.Rotate(rotate);
                }
                break;
            case NoteType.ARROWHORIZONTAL:
                {
                    timer += Time.deltaTime * 2;
                    Vector2 pos = basePos;
                    pos.x += Mathf.Sin(timer) / 4;
                    transform.position = pos;
                }
                break;
            case NoteType.ARROWVERTICAL:
                {
                    timer += Time.deltaTime * 2;
                    Vector2 pos = basePos;
                    pos.y += Mathf.Sin(timer) / 4;
                    transform.position = pos;
                }
                break;
            case NoteType.BULLETSTRAIGHT:
                break;
            case NoteType.BULLETTRACKING:
                {
                    Vector3 rotate = new Vector3(0f, 0f, 6f);
                    gameObject.transform.Rotate(rotate);
                }
                break;
            default:
                break;
        }
    }
}
