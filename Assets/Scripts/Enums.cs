using UnityEngine;

public enum AttackColor
{
    RED,
    BLUE,
    BLACK
}

public enum NoteType
{
    ARROWSTATIC,
    ARROWTRACKING,
    ARROWHORIZONTAL,
    ARROWVERTICAL,
    BULLETSTRAIGHT,
    BULLETTRACKING
}

public static class NoteTypeMethods
{
    public static NoteType FromScript(BeatMapNote note)
    {
        switch(note)
        {
            case BeatMapArrowStatic:
                return NoteType.ARROWSTATIC;
            case BeatMapArrowTracking:
                return NoteType.ARROWTRACKING;
            case BeatMapArrowHorizontal:
                return NoteType.ARROWHORIZONTAL;
            case BeatMapArrowVertical:
                return NoteType.ARROWVERTICAL;
        }

        return NoteType.BULLETSTRAIGHT;
    }
}

public enum NoteSuperType
{
    ARROW,
    BULLET
}

public enum ArrowMovementType { 
    STATIC,
    TRACKING,
    HORIZONTAL,
    VERTICAL
}

public enum BulleteMovementType
{
    STRAIGHT,
    TRACKING
}

public enum NoteSpawnMethod
{
    FADE,
    UPMOVINGFADE,
    DOWNMOVINGFADE,
    LEFTMOVINGFADE,
    RIGHTMOVINGFADE
}

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}