using UnityEngine;

public class BeatMapMakerTempArrowStatic : MonoBehaviour
{
    private bool tracking = true;

    private void Update()
    {
        if (tracking)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Vector2 direction = mousePos - transform.position;

            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90f;
            angle = angle / 15;
            angle = Mathf.Round(angle);
            angle = angle * 15;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void StopTracking()
    {
        tracking = false;
    }
}
