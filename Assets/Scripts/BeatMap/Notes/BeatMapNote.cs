using System;
using UnityEngine;

public abstract class BeatMapNote
{
    public NoteType Type;

    public float TimeStamp;
    public Vector2 SpawnPoint;
    public AttackColor Color;

    public abstract float SpawnTime();

    public string ID()
    {
        return TimeStamp.ToString() + SpawnPoint.ToString();
    }
}
