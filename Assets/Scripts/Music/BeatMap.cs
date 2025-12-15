using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeatMap", menuName = "BeatMaps/New BeatMap", order = 1)]
public class BeatMap : ScriptableObject
{
    [SerializeField]
    private List<ArrowMovementInstructions> arrowMap = new List<ArrowMovementInstructions>();
    public List<ArrowMovementInstructions> ArrowMap { get { return arrowMap; } }
}
