using System;
using UnityEngine;

public enum RotateDirection { CW, CCW}
public class RotateEventArgs : EventArgs
{
    public Touch Finger1 { get; private set; }
    public Touch Finger2 { get; private set; }
    public float Angle { get; private set; }
    public RotateDirection RotationDirection { get; private set; } = RotateDirection.CW;
    public GameObject HitObject { get; private set; } = null;

    public RotateEventArgs(Touch finger1, Touch finger2, float angle, RotateDirection rotateDirection, GameObject hitOject)
    {
        Finger1 = finger1;
        Finger2 = finger2;
        Angle = angle;
        RotationDirection = rotateDirection;
        HitObject = hitOject;
    }

}
