using UnityEngine;

public abstract class BeatMapBullet : BeatMapNote
{
    public float Delay;

    public float Speed;

    public NoteSpawnMethod SpawnMethod;

    public override float SpawnTime()
    {
        return TimeStamp - Delay;
    }
}
