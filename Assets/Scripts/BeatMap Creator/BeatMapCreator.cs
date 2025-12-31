using System.Collections.Generic;
using UnityEngine;

public class BeatMapCreator : MonoBehaviour
{
    [SerializeField] private MusicTimeTracker song;

    [SerializeField] private GameObject staticArrowObject;

    public List<BeatMapNote> notes = new List<BeatMapNote>();

    private GameObject tempNote;

    private bool placingNote = false;
    private int step = 0;

    private NoteType? noteType = null;
    private ArrowMovementType? arrowType = null;
    private BulleteMovementType? bulletType = null;

    private void Start()
    {
        notes = BeatMapTranslator.FromFileText(song.SongName());
    }

    private void Update()
    {
        if (placingNote)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (noteType == NoteType.ARROW)
                {
                    if (arrowType == ArrowMovementType.STATIC)
                    {
                        StaticArrowClick();
                    }
                }
            }
        }
    }

    public void StartPlacing(NoteType noteType, ArrowMovementType? arrowType, BulleteMovementType? bulletType)
    {
        placingNote = true;
        step = 0;
        this.noteType = noteType;
        this.arrowType = arrowType;
        this.bulletType = bulletType;
    }

    private void StaticArrowClick()
    {
        switch (step)
        {
            case 0:
                step++;
                break;
            case 1:
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    tempNote = Instantiate(staticArrowObject, mousePos, Quaternion.identity);
                    step++;
                }
                break;
            case 2:
                tempNote.GetComponent<BeatMapMakerTempArrowStatic>().StopTracking();
                BeatMapArrowStatic note = new BeatMapArrowStatic();
                note.ChargeTime = 0.5f;
                note.Color = AttackColor.BLACK;
                float angleDeg = tempNote.transform.rotation.z;
                float angleRad = angleDeg * Mathf.Deg2Rad;

                Vector2 direction = new Vector2(
                    Mathf.Cos(angleRad),
                    Mathf.Sin(angleRad)
                ).normalized;
                note.Direction = direction;
                note.SpawnMethod = ArrowSpawnMethods.FADE;
                note.SpawnPoint = tempNote.transform.position;
                note.TimeStamp = song.Timestamp;
                notes.Add(note);
                SortList();
                step++;
                break;
            default:
                break;
        }
    }

    private void SortList()
    {
        notes.Sort((a, b) => a.TimeStamp.CompareTo(b.TimeStamp));
    }

    public void SaveToFile()
    {
        string text = BeatMapTranslator.ToFileText(notes);

        FileWriter.WriteBeatMap(song.SongName(), text);
    }
}