using UnityEngine;

public class ArrowMovementInstructions : ScriptableObject
{
    [SerializeField]
    protected float noteTime = 0f;
    public float NoteTime { get { return noteTime; } }

    [SerializeField]
    protected float chargeTime = 0f;
    public float ChargeTime { get { return chargeTime; } }

    public float SpawnTime { get { return noteTime - chargeTime; } }

    [SerializeField]
    protected Vector3 spawnPoint;
    public Vector2 SpawnPoint { get { return spawnPoint; } }

    [SerializeField]
    protected ArrowSpawnMethods spawnMethod;
    public ArrowSpawnMethods SpawnMethod { get { return spawnMethod; } }

    [SerializeField]
    protected AttackColor arrowColor;
    public AttackColor ArrowColor { get { return arrowColor; } }
}
