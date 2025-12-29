using System.Collections.Generic;
using UnityEngine;

public class BeatMapCreator : MonoBehaviour
{
    [SerializeField] private MusicTimeTracker song;

    public List<BeatMapNote> notes = new List<BeatMapNote>();

    public void SaveToFile(string name)
    {
        string text = BeatMapTranslator.ToFileText(notes);

        FileWriter.WriteBeatMap(name, text);
    }
}