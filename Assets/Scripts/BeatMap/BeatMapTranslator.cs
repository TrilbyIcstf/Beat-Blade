using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class BeatMapTranslator
{
    public static string ToFileText(List<BeatMapNote> notes)
    {
        string text = "";
        foreach(BeatMapNote note in notes)
        {
            text += $"{note.GetType().Name}\n";
            text += $"{JsonUtility.ToJson(note)}\n";
            text += "|\n";
        }
        return text;
    }

    public static List<BeatMapNote> FromFileText(string name)
    {
        List<BeatMapNote> notes = new List<BeatMapNote>();

        string folderPath = "Assets/BeatMaps";
        string filePath = folderPath + $"/{name}.txt";

        if (File.Exists(filePath))
        {
            string fileText = File.ReadAllText(filePath);

            List<string> noteList = fileText.Split('|', System.StringSplitOptions.RemoveEmptyEntries).ToList();
            noteList.RemoveAt(noteList.Count - 1); // I don't wanna bother removing the last | in the file so removing the trailing note

            foreach(string note in noteList)
            {
                List<string> notePieces = note.Split("\n", System.StringSplitOptions.RemoveEmptyEntries).ToList();
                string type = notePieces[0];
                string jSON = notePieces[1];
                var noteObject = JsonUtility.FromJson(jSON, System.Type.GetType(type));

                notes.Add((BeatMapNote)noteObject);
            }
        }

        return notes;
    }
}
