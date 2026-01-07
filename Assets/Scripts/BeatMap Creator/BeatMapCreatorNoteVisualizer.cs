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
            time = song.Timestamp;

            List<BeatMapNote> invalidNotes = new List<BeatMapNote>();
            foreach(var note in notes)
            {
                if (!WithinTime(note.Key))
                {
                    invalidNotes.Add(note.Key);
                }
            }

            foreach(BeatMapNote note in invalidNotes)
            {
                Destroy(notes[note]);
                notes.Remove(note);
            }

            foreach(BeatMapNote note in creator.GetNotes())
            {
                if (WithinTime(note) && !notes.ContainsKey(note)) {
                    GameObject tempNote = Instantiate(creator.TempNote(note.Type), note.SpawnPoint, Quaternion.identity);
                    tempNote.GetComponent<BeatMapMakerTempNote>().StopTracking();
                    if (note is BeatMapArrowStatic n)
                    {
                        tempNote.GetComponent<BeatMapMakerTempNote>().RotateTowards(n.Direction);
                    }
                    notes[note] = tempNote;
                }
            }
        }
    }

    private bool WithinTime(BeatMapNote note)
    {
        float diff = Mathf.Abs(note.TimeStamp - time);

        return diff <= 0.25f;
    }
}
