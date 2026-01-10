using System;
using UnityEngine;

public abstract class BeatMapArrow : BeatMapNote
{
    public float ChargeTime;
    public NoteSpawnMethod SpawnMethod;

    public override float SpawnTime()
    {
        return TimeStamp - ChargeTime;
    }
}
