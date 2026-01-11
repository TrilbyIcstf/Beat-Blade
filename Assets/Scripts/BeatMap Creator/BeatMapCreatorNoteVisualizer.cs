using System.Collections.Generic;
using UnityEngine;

public class BeatMapCreatorNoteVisualizer : MonoBehaviour
{
    [SerializeField] private MusicTimeTracker song;
    [SerializeField] private BeatMapCreator creator;

    private Dictionary<BeatMapNote, GameObject> notes = new Dictionary<BeatMapNote, GameObject>();

    private float time = 0;

    private void Update()
    {
        if (time != song.Timestamp)
        {
            CheckNotes();
        }
    }

    public void CheckNotes()
    {
        time = song.Timestamp;

        List<BeatMapNote> invalidNotes = new List<BeatMapNote>();
        foreach (var note in notes)
        {
            if (!WithinTime(note.Key))
            {
                invalidNotes.Add(note.Key);
            }
        }

        foreach (BeatMapNote note in invalidNotes)
        {
            Destroy(notes[note]);
            notes.Remove(note);
        }

        foreach (BeatMapNote note in creator.GetNotes())
        {
            if (WithinTime(note) && !notes.ContainsKey(note))
            {
                GameObject tempNote = Instantiate(creator.TempNote(note.Type), note.SpawnPoint, Quaternion.identity);
                tempNote.GetComponent<BeatMapCreatorTempNote>().StopTracking();
                tempNote.GetComponent<BeatMapCreatorTempNoteEdit>().enabled = true;
                tempNote.GetComponent<BeatMapCreatorTempNoteEdit>().SetID(note.ID());
                tempNote.GetComponent<BeatMapCreatorTempNoteEdit>().SetTimestamp(note.TimeStamp);
                if (note is BeatMapArrowStatic n)
                {
                    tempNote.GetComponent<BeatMapCreatorTempNote>().RotateTowards(n.Direction);
                }
                else if (note is BeatMapBulletStraight b)
                {
                    tempNote.GetComponent<BeatMapCreatorTempNote>().RotateTowards(b.Direction);
                }
                notes[note] = tempNote;
            }
        }
    }

    private bool WithinTime(BeatMapNote note)
    {
        float diff = Mathf.Abs(note.TimeStamp - time);

        return diff <= 0.25f;
    }
}
