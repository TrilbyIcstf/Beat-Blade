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

public enum ArrowSpawnMethod
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