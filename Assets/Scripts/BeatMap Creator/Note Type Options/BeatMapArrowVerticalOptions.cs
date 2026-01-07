using UnityEngine;

public class BeatMapArrowVerticalOptions : BeatMapArrowOptions
{
    public override void Save()
    {
        BeatMapCreator creator = GameObject.FindGameObjectWithTag("BeatMap Creator").GetComponent<BeatMapCreator>();
        creator.VerticalArrowSave();
    }
}
