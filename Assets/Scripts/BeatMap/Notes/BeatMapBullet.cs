using UnityEngine;

public abstract class BeatMapBullet : BeatMapNote
{
    public float Delay;

    public ArrowSpawnMethod SpawnMethod;

    public override float SpawnTime()
    {
        return TimeStamp - Delay;
    }
}
