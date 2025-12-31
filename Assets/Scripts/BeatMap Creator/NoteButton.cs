using UnityEngine;

public class NoteButton : MonoBehaviour
{
    public NoteType noteType;
    public ArrowMovementType arrowType;
    public BulleteMovementType bulletType;

    private void OnMouseDown()
    {
        GameObject creatorObject = GameObject.FindGameObjectWithTag("BeatMap Creator");
        BeatMapCreator creator = creatorObject.GetComponent<BeatMapCreator>();
        creator.StartPlacing(noteType, arrowType, bulletType);
    }
}
