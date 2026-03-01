using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Collections.Generic;

public abstract class BeatMapBulletOptions : MonoBehaviour
{
    [SerializeField] TMP_Text delayText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Dropdown colorDropdown;
    [SerializeField] TMP_Dropdown enterTypeDropdown;

    private float delay = 0.5f;
    private float speed = 5;

    private void Start()
    {
        colorDropdown.ClearOptions();

        List<String> colors = Enum.GetNames(typeof(AttackColor)).ToList();

        colorDropdown.AddOptions(colors);
        colorDropdown.value = 2;

        enterTypeDropdown.ClearOptions();

        List<String> enterTypes = Enum.GetNames(typeof(NoteSpawnMethod)).ToList();

        enterTypeDropdown.AddOptions(enterTypes);
    }

    public abstract void Save();

    public void Cancel()
    {
        BeatMapCreator creator = GameObject.FindGameObjectWithTag("BeatMap Creator").GetComponent<BeatMapCreator>();
        creator.CancelNote();
    }

    public void SetDelay(float val)
    {
        float tempVal = val * 20;
        tempVal = Mathf.Round(tempVal);
        tempVal = tempVal / 20;
        delay = tempVal;
        delayText.text = $"Delay: {delay.ToString()}";
    }

    public float GetDelay()
    {
        return delay;
    }

    public void SetSpeed(float val)
    {
        float tempVal = val * 5;
        tempVal = Mathf.Round(tempVal);
        tempVal = tempVal / 5;
        speed = tempVal;
        speedText.text = $"Speed: {speed.ToString()}";
    }

    public float GetSpeed()
    {
        return speed;
    }

    public AttackColor GetColor()
    {
        return (AttackColor)System.Enum.Parse(typeof(AttackColor), colorDropdown.options[colorDropdown.value].text);
    }

    public NoteSpawnMethod GetSpawnMethod()
    {
        return (NoteSpawnMethod)System.Enum.Parse(typeof(NoteSpawnMethod), enterTypeDropdown.options[enterTypeDropdown.value].text);
    }
}
