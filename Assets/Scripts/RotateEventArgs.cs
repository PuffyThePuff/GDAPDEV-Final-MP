using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEventArgs : EventArgs
{
   public Touch Finger1 { get; private set; }
    public Touch Finger2 { get; private set; }
    public float Angle { get; private set; } = 0;

    public RotationDirections RotationDirection { get; private set; } = RotationDirections.CW;

    public GameObject HitObject { get; private set; } = null;

    public RotateEventArgs(Touch finger1, Touch finger2, float angle, RotationDirections rotDir, GameObject hitObj)
    {
        Finger1 = finger1;
        Finger2 = finger2;
        Angle = angle;
        RotationDirection = rotDir;
        HitObject = hitObj;
    }
}

public enum RotationDirections
{
    CW, //Clockwise
    CCW //Counter-Clockwise
}
