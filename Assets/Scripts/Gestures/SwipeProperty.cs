using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwipeProperty
{
    [Tooltip("Min Distance covered to be considered as a Swipe")]
    public float minSwipeDistance = 2f;

    [Tooltip("Max Gesture Time until it's not considered a Swipe")]
    public float swipeTime = 0.7f;
}
