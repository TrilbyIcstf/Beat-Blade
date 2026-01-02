using System.Collections.Generic;
using UnityEngine;

public class BeatMapCreator : MonoBehaviour
{
    [SerializeField] private MusicTimeTracker song;

    [SerializeField] private BeatMapCreatorNoteTypeDictionary noteTypes;

    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject buttonPrefab;

    public List<BeatMapNote> notes = new List<BeatMapNote>();

    private GameObject tempButtons;
    private GameObject tempNote;
    private GameObject tempOptions;

    private bool placingNote = false;
    private int step = 0;

    private NoteType? currentNote = null;

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
                switch (currentNote)
                {
                    case NoteType.ARROWSTATIC:
                        StaticArrowClick();
                        break;
                    case NoteType.ARROWTRACKING:
                        TrackingArrowClick();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void OpenButtons()
    {
        if (!placingNote) {
            tempButtons = Instantiate(buttonPrefab, transform.position, Quaternion.identity);
        }
    }

    public void StartPlacing(NoteType currentNote)
    {
        Destroy(tempButtons);
        placingNote = true;
        step = 0;
        this.currentNote = currentNote;
    }

    public void EndPlacing()
    {
        placingNote = false;
        step = 0;
        currentNote = null;
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
                    tempNote = Instantiate(TempNote(NoteType.ARROWSTATIC), mousePos, Quaternion.identity);
                    step++;
                }
                break;
            case 2:
                {
                    tempNote.GetComponent<BeatMapMakerTempArrowStatic>().StopTracking();
                    tempOptions = Instantiate(OptionsPrefab(NoteType.ARROWSTATIC), canvas.transform);
                    step++;
                }
                break;
            default:
                break;
        }
    }

    public void StaticArrowSave()
    {
        BeatMapArrowStatic note = new BeatMapArrowStatic();
        BeatMapArrowStaticOptions options = tempOptions.GetComponent<BeatMapArrowStaticOptions>();
        note.ChargeTime = options.GetDelay();
        note.Color = options.GetColor();
        float angleDeg = tempNote.transform.rotation.z;
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(
            Mathf.Cos(angleRad),
            Mathf.Sin(angleRad)
        ).normalized;
        note.Direction = direction;
        note.SpawnMethod = options.GetSpawnMethod();
        note.SpawnPoint = tempNote.transform.position;
        note.TimeStamp = song.Timestamp;
        notes.Add(note);
        SortList();

        Destroy(tempNote);
        Destroy(tempOptions);
        EndPlacing();
    }

    private void TrackingArrowClick()
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
                    tempNote = Instantiate(TempNote(NoteType.ARROWTRACKING), mousePos, Quaternion.identity);
                    tempOptions = Instantiate(OptionsPrefab(NoteType.ARROWTRACKING), canvas.transform);
                    step++;
                }
                break;
            default:
                break;
        }
    }

    public void TrackingArrowSave()
    {
        BeatMapArrowTracking note = new BeatMapArrowTracking();
        BeatMapArrowTrackingOptions options = tempOptions.GetComponent<BeatMapArrowTrackingOptions>();
        note.ChargeTime = options.GetDelay();
        note.Color = options.GetColor();
        float angleDeg = tempNote.transform.rotation.z;
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(
            Mathf.Cos(angleRad),
            Mathf.Sin(angleRad)
        ).normalized;
        note.SpawnMethod = options.GetSpawnMethod();
        note.SpawnPoint = tempNote.transform.position;
        note.TimeStamp = song.Timestamp;
        notes.Add(note);
        SortList();

        Destroy(tempNote);
        Destroy(tempOptions);
        EndPlacing();
    }

    public void CancelNote()
    {
        Destroy(tempNote);
        Destroy(tempOptions);
        EndPlacing();
    }

    private GameObject TempNote(NoteType type)
    {
        return noteTypes[type].tempNote;
    }

    private GameObject OptionsPrefab(NoteType type)
    {
        return noteTypes[type].optionsPrefab;
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