using System.Collections;
using UnityEngine;

public class PlayerAttackLine : MonoBehaviour
{
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
