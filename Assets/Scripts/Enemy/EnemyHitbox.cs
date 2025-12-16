using System.Collections;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    private Coroutine flashRoutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player Line")
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }

            flashRoutine = StartCoroutine(DamageFlash());
        }
    }

    private IEnumerator DamageFlash()
    {
        sr.enabled = true;
        yield return new WaitForSeconds(0.15f);
        sr.enabled = false;
    }
}
