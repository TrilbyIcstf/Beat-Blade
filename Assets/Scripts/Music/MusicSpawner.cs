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
                AttackArrow arrow = SpawnArrow(a);
                float chargeRatio = (time - a.SpawnTime()) / a.ChargeTime;
                arrow.SetChargeTo(chargeRatio);
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
        float secondNextTimestamp = float.MaxValue;
        foreach(GameObject note in activeNotes)
        {
            switch(note.tag)
            {
                case "Beat Arrow":
                    AttackArrow arrow = note.GetComponent<AttackArrow>();
                    if (arrow.GetTimestamp() < nextTimestamp)
                    {
                        secondNextTimestamp = nextTimestamp;
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
                    if (arrow.GetTimestamp() - nextTimestamp <= 0.05f)
                    {
                        arrow.SetHighlight(true, Color.white);
                    }
                    else if (arrow.GetTimestamp() - secondNextTimestamp <= 0.05f)
                    {
                        arrow.SetHighlight(true, Colors.BeatGrey);
                    }
                    else
                    {
                        arrow.SetHighlight(false, Color.black);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private AttackArrow SpawnArrow(BeatMapArrow arrow)
    {
        GameObject tempArrow = Instantiate(Arrow, arrow.SpawnPoint, Quaternion.identity);
        AttackArrow arrowScript = tempArrow.GetComponent<AttackArrow>();
        arrowScript.SetInstructions(arrow);

        activeNotes.Add(tempArrow);
        return arrowScript;
    }

    private BulletMarker SpawnBullet(BeatMapBullet bullet)
    {
        GameObject tempBullet = Instantiate(Bullet, bullet.SpawnPoint, Quaternion.identity);
        BulletMarker bulletScript = tempBullet.GetComponent<BulletMarker>();
        bulletScript.SetInstructions(bullet);

        activeNotes.Add(tempBullet);
        return bulletScript;
    }
}
