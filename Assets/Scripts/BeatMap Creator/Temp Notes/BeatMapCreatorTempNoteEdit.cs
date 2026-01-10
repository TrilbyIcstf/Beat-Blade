using UnityEngine;

public class BeatMapCreatorTempNoteEdit : MonoBehaviour
{
    private string ID;
    private float timestamp;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MusicTimeTracker song = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicTimeTracker>();
            song.SetTime(timestamp);
        }

        if (Input.GetMouseButtonDown(1) && ID != null)
        {
            BeatMapCreator creator = GameObject.FindGameObjectWithTag("BeatMap Creator").GetComponent<BeatMapCreator>();
            creator.RemoveNote(ID);
            Destroy(gameObject);
        }
    }

    public void SetID(string val)
    {
        ID = val;
    }

    public void SetTimestamp(float val)
    {
        timestamp = val;
    }
}
