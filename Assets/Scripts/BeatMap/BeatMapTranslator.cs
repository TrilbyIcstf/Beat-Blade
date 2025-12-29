using System.Collections.Generic;
using UnityEngine;

public class BeatMapTranslator
{
    public static string ToFileText(List<BeatMapNote> notes)
    {
        string text = "";
        foreach(BeatMapNote note in notes)
        {
            text += "<\n";
            text += $"{note.GetType().Name}\n";
            text += $"{JsonUtility.ToJson(note)}\n";
            text += ">\n";
        }
        return text;
    }
}
