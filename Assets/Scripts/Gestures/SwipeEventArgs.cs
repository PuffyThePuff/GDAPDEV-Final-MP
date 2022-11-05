using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeEventArgs : EventArgs
{
    private Vector2 swipePos;
    private SwipeDirections swipeDirection;
    private Vector2 swipeVector;
    private GameObject hitObject;

    public SwipeEventArgs(Vector2 _swipePos, SwipeDirections _swipeDirections,
                            Vector2 _swipeVector, GameObject _hitObject)
    {
        swipePos = _swipePos;
        swipeDirection = _swipeDirections;
        swipeVector = _swipeVector;
        hitObject = _hitObject;
    }

    public Vector2 SwipePos
    {
        get { return swipePos; }
    }

    public SwipeDirections SwipeDirection
    {
        get { return swipeDirection; }
    }

    public Vector2 SwipeVector
    {
        get { return swipeVector; }
    }

    public GameObject HitObject
    {
        get { return hitObject; }
    }

}

public enum SwipeDirections
{
    RIGHT,
    LEFT,
    UP,
    DOWN
}
