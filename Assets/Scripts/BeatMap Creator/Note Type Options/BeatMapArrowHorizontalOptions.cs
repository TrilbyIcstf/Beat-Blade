using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Collections.Generic;

public class BeatMapArrowHorizontalOptions : BeatMapArrowOptions
{
    public override void Save()
    {
        BeatMapCreator creator = GameObject.FindGameObjectWithTag("BeatMap Creator").GetComponent<BeatMapCreator>();
        creator.HorizontalArrowSave();
    }
}
