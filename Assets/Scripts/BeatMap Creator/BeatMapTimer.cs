using UnityEngine;
using TMPro;

public class BeatMapTimer : MonoBehaviour
{
    [SerializeField] private MusicTimeTracker song;

    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.text = song.Timestamp.ToString();
    }
}
