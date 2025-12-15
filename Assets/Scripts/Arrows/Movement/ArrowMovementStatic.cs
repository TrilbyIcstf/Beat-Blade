using UnityEngine;

[CreateAssetMenu(fileName = "Static Arrow", menuName = "Movement Instructions/New Static Arrow", order = 1)]
public class ArrowMovementStatic : ArrowMovementInstructions
{
    [SerializeField]
    private Vector2 direction;
    public Vector2 Direction { get { return direction; } }
}
