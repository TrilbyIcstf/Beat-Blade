using System;
using UnityEngine;

public abstract class BeatMapArrow : BeatMapNote
{
    public float ChargeTime;
    public ArrowSpawnMethod SpawnMethod;

    public override float SpawnTime()
    {
        return TimeStamp - ChargeTime;
    }
}
