using System;
using UnityEngine;

public class TapEventArgs : EventArgs
{
    public Vector2 TapPosition { get; private set; }
    public GameObject HitObject { get; private set; }

    public TapEventArgs(Vector2 tapPosition, GameObject hitObj = null)
    {
        TapPosition = tapPosition;
        HitObject = hitObj;
    }
}
