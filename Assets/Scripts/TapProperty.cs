using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TapProperty
{
    [Tooltip("Max allowable time until it's not a tap anymore")]
    public float tapTime = 0.7f;

    [Tooltip("Max allowable distance until it's not a tap anymore")]
    public float tapMaxDistance = 0.1f;
}
