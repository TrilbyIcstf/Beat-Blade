using UnityEngine;

public class BeatMapMakerTempArrowTracking : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 toPlayer = player.transform.position - transform.position;

        RotateTowards(toPlayer);

    }

    private void RotateTowards(Vector3 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 90);

        transform.rotation = rotation;
    }
}
