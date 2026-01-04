using System.Collections.Generic;
using UnityEngine;

public class MusicSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Arrow;

    [SerializeField]
    private BeatMap beatMap;

    private List<BeatMapNote> noteMap;

    private MusicTimeTracker music;

    private void Awake()
    {
        music = GetComponent<MusicTimeTracker>();
        noteMap = BeatMapTranslator.FromFileText(music.SongName());
    }

    private void Update()
    {
        UpdateBeat(music.Timestamp);
    }

    public void UpdateBeat(float time)
    {
        while(noteMap.Count > 0 && noteMap[0].SpawnTime() <= time)
        {
            BeatMapNote note = noteMap[0];
            noteMap.RemoveAt(0);

            if (note is BeatMapArrow a) 
            {
                SpawnArrow(a);
            }
        }
    }

    private void SpawnArrow(BeatMapArrow arrow)
    {
        GameObject tempArrow = Instantiate(Arrow, arrow.SpawnPoint, Quaternion.identity);
        tempArrow.GetComponent<AttackArrow>().SetInstructions(arrow);
    }
}
