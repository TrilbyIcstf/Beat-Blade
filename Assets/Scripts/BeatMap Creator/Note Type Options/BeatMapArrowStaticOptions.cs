using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Collections.Generic;

public class BeatMapArrowStaticOptions : MonoBehaviour
{
    [SerializeField] TMP_Text delayText;
    [SerializeField] TMP_Dropdown colorDropdown;
    [SerializeField] TMP_Dropdown enterTypeDropdown;

    private float delay = 1;

    private void Start()
    {
        colorDropdown.ClearOptions();

        List<String> colors = Enum.GetNames(typeof(AttackColor)).ToList();

        colorDropdown.AddOptions(colors);

        enterTypeDropdown.ClearOptions();

        List<String> enterTypes = Enum.GetNames(typeof(ArrowSpawnMethod)).ToList();

        enterTypeDropdown.AddOptions(enterTypes);
    }

    public void Save()
    {
        BeatMapCreator creator = GameObject.FindGameObjectWithTag("BeatMap Creator").GetComponent<BeatMapCreator>();
        creator.StaticArrowSave();
    }

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
        delayText.text = delay.ToString();
    }

    public float GetDelay()
    {
        return delay;
    }

    public AttackColor GetColor()
    {
        return (AttackColor)System.Enum.Parse(typeof(AttackColor), colorDropdown.options[colorDropdown.value].text);
    }
    
    public ArrowSpawnMethod GetSpawnMethod()
    {
        return (ArrowSpawnMethod)System.Enum.Parse(typeof(ArrowSpawnMethod), enterTypeDropdown.options[enterTypeDropdown.value].text);
    }
}
