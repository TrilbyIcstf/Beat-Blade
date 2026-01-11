using UnityEngine;

public class BeatMapBulletStraightOptions : BeatMapBulletOptions
{
    public override void Save()
    {
        BeatMapCreator creator = GameObject.FindGameObjectWithTag("BeatMap Creator").GetComponent<BeatMapCreator>();
        creator.StraightBulletSave();
    }
}
