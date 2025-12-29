using System.Collections.Generic;
using UnityEngine;

public class MusicSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Arrow;

    [SerializeField]
    private BeatMap beatMap;

    private List<ArrowMovementInstructions> arrowMap;

    private MusicTimeTracker music;

    private void Awake()
    {
        arrowMap = new List<ArrowMovementInstructions>(beatMap.ArrowMap);
        music = GetComponent<MusicTimeTracker>();
    }

    private void Update()
    {
        UpdateBeat(music.Timestamp);
    }

    public void UpdateBeat(float time)
    {
        while(arrowMap.Count > 0 && arrowMap[0].SpawnTime <= time)
        {
            ArrowMovementInstructions arrow = arrowMap[0];
            arrowMap.RemoveAt(0);

            GameObject tempArrow = Instantiate(Arrow, arrow.SpawnPoint, Quaternion.identity);
            tempArrow.GetComponent<AttackArrow>().SetInstructions(arrow);
        }
    }
}
