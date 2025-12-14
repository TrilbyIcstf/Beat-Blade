using UnityEngine;

public class AttackLine : MonoBehaviour
{
    [SerializeField]
    private AttackColor attackColor = AttackColor.BLACK;
    public AttackColor AttackColor
    {
        get { return attackColor; }
        set { attackColor = value; }
    }
}
