using System.Collections;
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
