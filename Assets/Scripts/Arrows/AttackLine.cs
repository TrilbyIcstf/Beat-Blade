using System.Collections;
using UnityEngine;

public class AttackLine : MonoBehaviour
{
    [SerializeField]
    private AttackColor attackColor = AttackColor.BLACK;
    public AttackColor AttackColor
    {
        get { return attackColor; }
        set { 
            attackColor = value;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            switch (attackColor)
            {
                case AttackColor.BLACK:
                    sr.color = Color.black;
                    break;
                case AttackColor.RED:
                    sr.color = Color.red;
                    break;
                case AttackColor.BLUE:
                    sr.color = Color.blue;
                    break;
            }
        }
    }

    private void Awake()
    {
        StartCoroutine(DIE());
    }

    private IEnumerator DIE()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
