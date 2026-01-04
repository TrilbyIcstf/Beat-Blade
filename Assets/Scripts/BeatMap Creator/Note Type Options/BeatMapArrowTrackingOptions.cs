using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BeatMapArrowTrackingOptions : BeatMapArrowOptions
{
    public override void Save()
    {
        BeatMapCreator creator = GameObject.FindGameObjectWithTag("BeatMap Creator").GetComponent<BeatMapCreator>();
        creator.TrackingArrowSave();
    }
}
