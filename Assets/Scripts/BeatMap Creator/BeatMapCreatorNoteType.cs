using UnityEngine;

[CreateAssetMenu(fileName = "BeatMapCreatorNoteType", menuName = "BeatMap Creator/Note Type", order = 1)]
public class BeatMapCreatorNoteType : ScriptableObject
{
    public NoteType type;
    public GameObject optionsPrefab;
    public GameObject tempNote;
}
