using UnityEngine;

public class NoteButton : MonoBehaviour
{
    public NoteType noteType;

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
                Vector3 rotate = new Vector3(0f, 0f, 6f);
                gameObject.transform.Rotate(rotate);
                break;
            default:
                break;
        }
    }
}
