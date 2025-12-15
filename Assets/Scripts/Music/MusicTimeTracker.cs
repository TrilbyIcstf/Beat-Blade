using UnityEngine;

public class MusicTimeTracker : MonoBehaviour
{
    private AudioSource song;
    private MusicSpawner spawner;

    private float timestamp = 0f;

    private void Awake()
    {
        song = GetComponent<AudioSource>();
        spawner = GetComponent<MusicSpawner>();
        song.Play();
    }

    private void Update()
    {
        timestamp = song.time;
        Debug.Log(timestamp);
        spawner.UpdateBeat(timestamp);

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            song.time -= 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Period))
        {
            song.time += 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            song.pitch -= 0.2f;
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            song.pitch += 0.2f;
        }
    }
}
