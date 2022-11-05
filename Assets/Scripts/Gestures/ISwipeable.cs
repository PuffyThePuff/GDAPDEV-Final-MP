using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwipeable
{
    void OnSwipe(object sender, SwipeEventArgs swipeEvents);
}
