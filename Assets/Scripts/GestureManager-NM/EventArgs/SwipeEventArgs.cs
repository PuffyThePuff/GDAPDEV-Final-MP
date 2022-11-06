using System;
using UnityEngine;

public enum SwipeDirection 
{
    RIGHT,
    LEFT,
    UP,
    DOWN
}
public class SwipeEventArgs : EventArgs
{
    public SwipeDirection SwipeDirection { get; private set; }
    public Vector2 SwipePos { get; private set; }
    public GameObject HitObject { get; private set; }
    public Vector2 SwipeVector { get; private set; }

    public SwipeEventArgs(SwipeDirection direction, Vector2 swipePos, Vector2 swipeVector, GameObject hitObj = null)
    {
        this.SwipeDirection = direction;
        this.SwipePos = swipePos;
        this.SwipeVector = swipeVector;
        this.HitObject = hitObj;
    }
}
