using UnityEngine;

public class MusicTimeTracker : MonoBehaviour
{
    [SerializeField] private bool debug = false;

    private AudioSource song;
    private MusicSpawner spawner;

    public float Timestamp { get; private set; } = 0f;

    private bool paused = false;

    private void Awake()
    {
        song = GetComponent<AudioSource>();
        song.Play();
    }

    private void Update()
    {
        Timestamp = song.time;

        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                if (Timestamp < 0.5f)
                {
                    song.time = 0;
                }
                else
                {
                    song.time -= 0.5f;
                }
            }

            if (Input.GetKeyDown(KeyCode.Period))
            {
                song.time += 0.5f;
            }

            if (Input.GetKeyDown(KeyCode.Slash))
            {
                if (paused)
                {
                    song.UnPause();
                    paused = false;
                } 
                else
                {
                    song.Pause();
                    paused = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                song.pitch -= 0.2f;
            }

            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                song.pitch += 0.2f;
            }

            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                Debug.Log(Timestamp);
            }
        }
    }

    public string SongName()
    {
        return song.clip.name;
    }
}
