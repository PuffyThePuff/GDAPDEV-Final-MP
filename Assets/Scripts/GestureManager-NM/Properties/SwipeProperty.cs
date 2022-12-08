using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwipeProperty
{
    [Tooltip("Max Distance covered to be considered a Swipe")]
    public float minSwipeDistance = 1f;
    [Tooltip("Max gesture time until it is not longer a Swipe")]
    public float swipeTime = 1f;
}
