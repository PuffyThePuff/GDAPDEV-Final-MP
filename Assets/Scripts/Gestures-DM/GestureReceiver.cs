using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureReceiver : MonoBehaviour, ISwipeable, ISpread, IRotate
{

    // Start is called before the first frame update
    void Start()
    {
        GestureManager.Instance.OnSwipe += OnSwipe;
        GestureManager.Instance.OnSpread += OnSpread;
        GestureManager.Instance.OnRotate += OnRotate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnSwipe -= OnSwipe;
        GestureManager.Instance.OnSpread -= OnSpread;
        GestureManager.Instance.OnRotate -= OnRotate;
    }

    public void OnRotate(object sender, RotateEventArgs rotateEventArgs)
    {
        Debug.Log("Rotate Gesture Received");
      
    }

    public void OnSpread(object sender,SpreadEventArgs spreadEventArgs)
    {
        Debug.Log("Spread Gesture Received");
      
    }

    public void OnSwipe(object sender, SwipeEventArgs swipeEventArgs)
    {
        Debug.Log("Swipe Gesture Received");

    }


}
