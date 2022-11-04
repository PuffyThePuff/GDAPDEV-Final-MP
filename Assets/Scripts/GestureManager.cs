using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance;

    private Vector2 startPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private float gestureTime = 0;
    public bool gestureCompleted = false;

    //Event Handlers
    public EventHandler<TapEventArgs> OnTap;
    public EventHandler<SwipeEventArgs> OnSwipe;
    public EventHandler<SpreadEventArgs> OnSpread;
    public EventHandler<RotateEventArgs> OnRotate;

    //Gesture Properties
    public TapProperty _tapProperty;
    public SwipeProperty _swipeProperty;
    public SpreadProperty _spreadProperty;
    public RotateProperty _rotateProperty;


    //Tracked Fingers
    private Touch trackedFinger1;
    private Touch trackedFinger2;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.touchCount > 0)
        {
           if(Input.touchCount == 1)
            {
                CheckSingleFingerGestures();
            }
           else if(Input.touchCount > 1)
            {
                trackedFinger1 = Input.GetTouch(0);
                trackedFinger2 = Input.GetTouch(1);

                if(trackedFinger1.phase == TouchPhase.Moved || trackedFinger2.phase == TouchPhase.Moved)
                {
                    Vector2 prevPoint1 = GetPreviousPoint(trackedFinger1);
                    Vector2 prevPoint2 = GetPreviousPoint(trackedFinger2);

                    float currDistance = Vector2.Distance(trackedFinger1.position, trackedFinger2.position);
                    float prevDistance = Vector2.Distance(prevPoint1, prevPoint2);

                    if(Mathf.Abs(currDistance - prevDistance) >= (_spreadProperty.MinDistanceChange * Screen.dpi))
                    {
                        FireSpreadEvent(currDistance - prevDistance);
                        

                    }
                }

                if((trackedFinger1.phase == TouchPhase.Moved||trackedFinger2.phase == TouchPhase.Moved) &&
                    (Vector2.Distance(trackedFinger1.position,trackedFinger2.position) >=
                    (_rotateProperty.minDistance * Screen.dpi)))
                {
                    Vector2 prevPoint1 = GetPreviousPoint(trackedFinger1);
                    Vector2 prevPoint2 = GetPreviousPoint(trackedFinger2);

                    Vector2 diffVector = trackedFinger1.position - trackedFinger2.position;
                    Vector2 prevDiffVector = prevPoint1 - prevPoint2;

                    float angle = Vector2.Angle(prevDiffVector, diffVector);

                    if(angle >= _rotateProperty.minChange)
                    {
                        FireRotateEvent(diffVector, prevDiffVector, angle);
                        
                    }
                }

            }
        }
       
    }

    private Vector2 GetPreviousPoint(Touch finger)
    {
        return finger.position - finger.deltaPosition;
    }

    private Vector2 GetMidpoint(Vector2 p1, Vector2 p2)
    {
        return (p1 + p2) / 2;
    }

    private void FireRotateEvent(Vector2 diffVector, Vector2 prevDiffVector, float angle)
    {
        Vector3 cross = Vector3.Cross(prevDiffVector, diffVector);
        RotationDirections rotDir = RotationDirections.CW;

        if (cross.z > 0)
        {
            Debug.Log($"Rotate CCW {angle}");
            rotDir = RotationDirections.CCW;
        }
        else
        {
            Debug.Log($"Rotate CW {angle}");
            rotDir = RotationDirections.CW;
        }

        Vector2 mid = GetMidpoint(trackedFinger1.position, trackedFinger2.position);

        Ray r = Camera.main.ScreenPointToRay(mid);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if(Physics.Raycast(r,out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        RotateEventArgs rotateEventArgs = new RotateEventArgs(trackedFinger1, trackedFinger2,
                                                                angle, rotDir, hitObj);


        if(OnRotate != null)
        {
            OnRotate(this, rotateEventArgs);
        }

        if(hitObj != null)
        {
            IRotate rotate = hitObj.GetComponent<IRotate>();
            if(rotate != null)
            {
                rotate.OnRotate(this,rotateEventArgs);
            }
        }

    }

    private void FireSpreadEvent(float distance)
    {
        Debug.Log("Pinch/Spread");

        if (distance > 0)
        {
            Debug.Log("Spread");
        }
        else
        {
            Debug.Log("Pinch");
        }

        Vector2 midPoint = GetMidpoint(trackedFinger1.position, trackedFinger2.position);

        Ray r = Camera.main.ScreenPointToRay(midPoint);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if(Physics.Raycast(r,out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        SpreadEventArgs spreadEventArgs = new SpreadEventArgs(trackedFinger1, trackedFinger1, distance, hitObj);

        if(OnSpread != null)
        {
            OnSpread(this, spreadEventArgs);
        }

        if(hitObj != null)
        {
            ISpread spread = hitObj.GetComponent<ISpread>();
            if(spread != null)
            {
                spread.OnSpread(this,spreadEventArgs);
            }
        }

    }

    private void CheckSingleFingerGestures()
    {
        trackedFinger1 = Input.GetTouch(0);
        if (trackedFinger1.phase == TouchPhase.Began)
        {
            startPoint = trackedFinger1.position;
            gestureTime = 0;
        }
        else if (trackedFinger1.phase == TouchPhase.Ended)
        {
            endPoint = trackedFinger1.position;

            if (gestureTime <= _tapProperty.tapTime &&
                Vector2.Distance(startPoint, endPoint) <= Screen.dpi * _tapProperty.tapMaxDistance)
            {
                FireTapEvent(endPoint);
            }
            else if (gestureTime <= _swipeProperty.swipeTime &&
                Vector2.Distance(startPoint, endPoint) >= (_swipeProperty.minSwipeDistance * Screen.dpi))
            {
                FireSwipeEvent();
                
            }

        }
        else
        {
            gestureTime += Time.deltaTime;
        }
    }

    private void FireTapEvent(Vector2 tapPosition)
    {
        Debug.Log("Tap Event Fired");

        Ray r = Camera.main.ScreenPointToRay(tapPosition);
        RaycastHit hit;
        GameObject hitObj = null;

        if(Physics.Raycast(r,out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        if(OnTap != null)
        {
            TapEventArgs tapEvent = new TapEventArgs(tapPosition,hitObj);
            OnTap(this, tapEvent);
        }

        if(hitObj != null)
        {
            ITappable tappableObj = hitObj.GetComponent<ITappable>();
            if(tappableObj != null) { tappableObj.OnTap(); }
        }
    }

    private void FireSwipeEvent()
    {
        Debug.Log("Swipe Event Fired");
        Vector2 diff = endPoint - startPoint;

        Ray r = Camera.main.ScreenPointToRay(startPoint);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if(Physics.Raycast(r,out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        SwipeDirections swipeDir = SwipeDirections.RIGHT;

        if(Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            if(diff.x > 0)
            {
                Debug.Log("Swiped Right");
                swipeDir = SwipeDirections.RIGHT;
            }
            else
            {
                Debug.Log("Swiped Left");
                swipeDir = SwipeDirections.LEFT;
            }
        }
        else
        {
            if (diff.y > 0)
            {
                Debug.Log("Swiped Up");
                swipeDir = SwipeDirections.UP;
            }
            else
            {
                Debug.Log("Swiped Down");
                swipeDir = SwipeDirections.DOWN;
            }
        }

        SwipeEventArgs swipeEventArgs = new SwipeEventArgs(startPoint, swipeDir, diff, hitObj);
        if(OnSwipe != null)
        {
            OnSwipe(this, swipeEventArgs);
        }

        if (hitObj != null)
        {
            ISwipeable swipeable = hitObj.GetComponent<ISwipeable>();
            if(swipeable != null)
            {
                swipeable.OnSwipe(this,swipeEventArgs);
            }
        }



    }

    private void OnDrawGizmos()
    {
        int touchCount = Input.touchCount;
        if(touchCount > 0)
        {
            Ray r = Camera.main.ScreenPointToRay(trackedFinger1.position);
            Gizmos.DrawIcon(r.GetPoint(10), "spellun-sprite1_2");

            if(Input.touchCount > 1)
            {
                Ray r2 = Camera.main.ScreenPointToRay(trackedFinger2.position);
                Gizmos.DrawIcon(r2.GetPoint(10), "spellun-sprite1_0");
            }
        }
    }

}
