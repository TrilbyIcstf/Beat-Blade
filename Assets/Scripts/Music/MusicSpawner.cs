using System.Collections.Generic;
using UnityEngine;

public class MusicSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Arrow;

    [SerializeField]
    private GameObject Bullet;

    private List<BeatMapNote> noteMap;
    private List<GameObject> activeNotes = new List<GameObject>();

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

    private void LateUpdate()
    {
        HighlightNext(music.Timestamp);
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
            else if (note is BeatMapBullet b)
            {
                SpawnBullet(b);
            }
        }
    }

    public void HighlightNext(float time)
    {
        activeNotes.RemoveAll(o => o == null);

        float nextTimestamp = float.MaxValue;
        foreach(GameObject note in activeNotes)
        {
            switch(note.tag)
            {
                case "Beat Arrow":
                    AttackArrow arrow = note.GetComponent<AttackArrow>();
                    if (arrow.GetTimestamp() < nextTimestamp)
                    {
                        nextTimestamp = arrow.GetTimestamp();
                    }
                    break;
                default:
                    break;
            }
        }
        
        foreach(GameObject note in activeNotes)
        {
            switch(note.tag)
            {
                case "Beat Arrow":
                    AttackArrow arrow = note.GetComponent<AttackArrow>();
                    arrow.SetHighlight(arrow.GetTimestamp() == nextTimestamp);
                    break;
                default:
                    break;
            }
        }
    }

    private void SpawnArrow(BeatMapArrow arrow)
    {
        GameObject tempArrow = Instantiate(Arrow, arrow.SpawnPoint, Quaternion.identity);
        tempArrow.GetComponent<AttackArrow>().SetInstructions(arrow);

        activeNotes.Add(tempArrow);
    }

    private void SpawnBullet(BeatMapBullet bullet)
    {
        GameObject tempBullet = Instantiate(Bullet, bullet.SpawnPoint, Quaternion.identity);
        tempBullet.GetComponent<BulletMarker>().SetInstructions(bullet);

        activeNotes.Add(tempBullet);
    }
}
