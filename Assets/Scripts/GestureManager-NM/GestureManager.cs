using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Gestures { NONE, TAP, SWIPE, DRAG, PAN, SPREAD, ROTATE }
public class GestureManager : MonoBehaviour
{
    #region Singleton
    public static GestureManager Instance { get; private set; }

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public EventHandler<TapEventArgs> OnTap;
    public EventHandler<SwipeEventArgs> OnSwipe;
    public EventHandler<DragEventArgs> OnDrag;
    public EventHandler<TwoFingerPanEventArgs> OnTwoFingerPan;
    public EventHandler<SpreadEventArgs> OnSpread;
    public EventHandler<RotateEventArgs> OnRotate;

    public TapProperty _tapProperty;
    public SwipeProperty _swipeProperty;
    public DragProperty _dragProperty;
    public TwoFingerPanProperty _twoFingerPanProperty;
    public SpreadProperty _spreadProperty;
    public RotateProperty _rotateProperty;


    protected Vector2 startPoint = Vector2.zero;
    protected Vector2 endPoint = Vector2.zero;
    protected float gestureTime = 0f;
    protected Touch trackedFinger1;
    protected Touch trackedFinger2;

    protected Gestures gestures;
    public bool isPressed = false;

    private Crosshair cHair;

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        cHair = Crosshair.Instance;
    }

    void Update()
    {
#if true
        if (cHair.CrosshairState == CrosshairState.moving)
        {
            if (Input.touchCount > 1)
            {
                if (Input.touchCount == 2)
                {
                    CheckSingleFingerGestures();
                }
                else if (Input.touchCount > 2)
                {
                    CheckMultipleFingerGestures();
                }
            }
            else
            {
                isPressed = false;
            }
        }
        else if(cHair.CrosshairState == CrosshairState.stopped)
        {
            if (Input.touchCount > 0)
            {
                if (Input.touchCount == 1)
                {
                    CheckSingleFingerGestures();
                }
                else if (Input.touchCount > 1)
                {
                    CheckMultipleFingerGestures();
                }
            }
            else
            {
                isPressed = false;
            }
        }
#endif
    }

    protected virtual void CheckSingleFingerGestures()
    {
        trackedFinger1 = Input.GetTouch(0);

        if (trackedFinger1.phase == TouchPhase.Began)
        {
            startPoint = trackedFinger1.position;
            gestureTime = 0;
        }
        else if(trackedFinger1.phase == TouchPhase.Ended)
        {
            endPoint = trackedFinger1.position;

            //Tap Gesture
            if (gestureTime <= _tapProperty.tapTime && Vector2.Distance(startPoint, endPoint) <= _tapProperty.tapMaxDistance * Screen.dpi)
            {
                FireTapEvent(endPoint);
            }
            else if(gestureTime <= _swipeProperty.swipeTime &&
                Vector2.Distance(startPoint, endPoint) >= _swipeProperty.minSwipeDistance * Screen.dpi)
            {
                //Debug.Log(Vector2.Distance(startPoint, endPoint).ToString());
                FireSwipeEvent();
            }

        }
        else
        {
            gestureTime += Time.deltaTime;
            //Drag Gesture
            if(gestureTime >= _dragProperty.bufferTime)
            {
                FireDragEvent();            
            }
        }
    }

    protected virtual void CheckMultipleFingerGestures()
    {
        trackedFinger1 = Input.GetTouch(0);
        trackedFinger2 = Input.GetTouch(1);

        if (trackedFinger1.phase == TouchPhase.Moved && trackedFinger2.phase == TouchPhase.Moved &&
            Vector2.Distance(trackedFinger1.position, trackedFinger2.position) <= _twoFingerPanProperty.maxDistance * Screen.dpi)
        {
            FireTwoFingerPanEvent();
        }
        //Spread
        if ((trackedFinger1.phase == TouchPhase.Moved && trackedFinger2.phase == TouchPhase.Moved))
        {
            Vector2 prevPoint1 = GetPreviousPoint(trackedFinger1);
            Vector2 prevPoint2 = GetPreviousPoint(trackedFinger2);

            float currDistance = Vector2.Distance(trackedFinger1.position, trackedFinger2.position);
            float prevDistance = Vector2.Distance(prevPoint1, prevPoint2);

            if (Math.Abs(currDistance - prevDistance) >= _spreadProperty.MinDistanceChange * Screen.dpi)
            {
                FireSpreadEvent(currDistance - prevDistance);
            }
        }
        //Rotate Event
        if ((trackedFinger1.phase == TouchPhase.Moved && trackedFinger2.phase == TouchPhase.Moved)
            && Vector2.Distance(trackedFinger1.position, trackedFinger2.position) >= _rotateProperty.minDistance * Screen.dpi)
        {
            Vector2 prevPoint1 = GetPreviousPoint(trackedFinger1);
            Vector2 prevPoint2 = GetPreviousPoint(trackedFinger2);

            Vector2 diffVector = trackedFinger1.position - trackedFinger2.position;
            Vector2 prevDiffVector = prevPoint1 - prevPoint2;

            float angle = Vector2.Angle(prevDiffVector, diffVector);

            if (angle >= _rotateProperty.minChange)
            {
                FireRotateEvent(diffVector, prevDiffVector, angle);
            }
        }
    }

    protected void FireTapEvent(Vector2 pos)
    {
        Debug.Log("Tap!");
        GameObject hitObject = Crosshair.Instance.hitObject;
#if false
        Ray r = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if(Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObject = hit.collider.gameObject;
        }
#endif

        TapEventArgs tapArgs = new TapEventArgs(pos, hitObject);
        // Notify tap listeners with tap event
        // Check if anything is listening first
        if (OnTap != null)
            OnTap(this, tapArgs);

        if(hitObject != null)
        {
            if(hitObject.TryGetComponent<ITappable>(out ITappable tappable))
            {
                //Debug.Log($"Hit {hitObject.name}");
                tappable.OnTap();
            }
        }
        isPressed = true;
    }

    protected void FireSwipeEvent()
    {
        Debug.Log("Swipe");
        Vector2 direction = endPoint - startPoint;
        SwipeDirection swipeDir;

        //Horizontal Swipe
        if(Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                //Debug.Log("Right");
                swipeDir = SwipeDirection.RIGHT;
            }
            else
            {
                //Debug.Log("Left");
                swipeDir = SwipeDirection.LEFT;
            }
        }
        else //Verticle Swipe
        {
            if (direction.y > 0)
            {
                //Debug.Log("Up");
                swipeDir = SwipeDirection.UP;
            }
            else
            {
                //Debug.Log("Down");
                swipeDir = SwipeDirection.DOWN;
            }
        }

        GameObject hitObj = cHair.hitObject;
#if false
        Ray r = Camera.main.ScreenPointToRay(startPoint);
        RaycastHit hit;
        if(Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }
#endif

        SwipeEventArgs swipeArgs = new SwipeEventArgs(swipeDir, startPoint, direction, hitObj);
        if(OnSwipe != null)
        {
            OnSwipe(this, swipeArgs);
        }

        if(hitObj != null)
        {
            if(hitObj.TryGetComponent<ISwipeable>(out ISwipeable swipeable))
            {
                swipeable.OnSwipe(swipeArgs);
                Debug.Log(hitObj.name);
            }
        }
        isPressed = true;

       
    }

    protected void FireDragEvent()
    {
        Debug.Log("Drag");
#if false
        Ray r = Camera.main.ScreenPointToRay(trackedFinger1.position);
        RaycastHit hit;
        GameObject hitObj = null;

        if(Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }
#endif
        GameObject hitObj = Crosshair.Instance.hitObject;

        DragEventArgs dragEvent = new DragEventArgs(trackedFinger1, hitObj);

        if(OnDrag != null)
        {
            OnDrag(this, dragEvent);
        }

        if(hitObj != null)
        {
            if(hitObj.TryGetComponent<IDraggable>(out IDraggable draggable))
            {
                draggable.OnDrag(dragEvent);
            }
        }
        isPressed = true;
    }

    protected void FireTwoFingerPanEvent()
    {
        TwoFingerPanEventArgs args = new TwoFingerPanEventArgs(trackedFinger1, trackedFinger2);
        if(OnTwoFingerPan != null)
        {
            OnTwoFingerPan(this, args);
        }
        isPressed = true;
    }

    protected Vector2 GetPreviousPoint(Touch finger)
    {
        return finger.position - finger.deltaPosition;
    }

    protected Vector2 GetMidpoint(Vector2 p1, Vector2 p2)
    {
        return (p1 + p2) / 2;
    }

    protected void FireSpreadEvent(float distDelta)
    {
        SpreadOrPinch SorP = SpreadOrPinch.SPREAD;
        if(distDelta > 0)
        {
            SorP = SpreadOrPinch.SPREAD;
            Debug.Log("Spread");
        }
        else
        {
            SorP = SpreadOrPinch.PINCH;
            Debug.Log("Pinch");
        }

        Vector2 midPoint = GetMidpoint(trackedFinger1.position, trackedFinger2.position);
#if false
        Ray r = Camera.main.ScreenPointToRay(midPoint);
        RaycastHit hit;
        GameObject hitObj = null;
        Debug.DrawRay(r.origin, r.direction * 100, Color.red, 5f);

        if(Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
            Debug.Log("Hit object");
        }
#endif

        GameObject hitObject = Crosshair.Instance.hitObject;
        SpreadEventArgs args = new SpreadEventArgs(trackedFinger1, trackedFinger2, distDelta, hitObject, SorP);

        if(OnSpread != null) OnSpread(this, args);

        if(hitObject != null)
        {
            if(hitObject.TryGetComponent(out ISpreadable spreadable))
            {
                Debug.Log("Spreadable hit");
                spreadable.OnSpread(args);
            }
        }
        isPressed = true;
    }

    protected void FireRotateEvent(Vector2 diffVector, Vector2 prevDiffVector, float angle)
    {
        Debug.Log("Successful Rotate");
        Vector3 cross = Vector3.Cross(prevDiffVector, diffVector);
        RotateDirection dir;
        if(cross.z > 0)
        {
            //Debug.Log($"Rotate CCW {angle}");
            dir = RotateDirection.CCW;
        }
        else
        {
            //Debug.Log($"Rotate CW {angle}");
            dir = RotateDirection.CW;
        }
#if false
        GameObject hitObj = null;
        Vector2 mid = GetMidpoint(trackedFinger1.position, trackedFinger2.position);
        Ray r = Camera.main.ScreenPointToRay(mid);
        RaycastHit hit;

        if(Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }
#endif

        GameObject hitObject = Crosshair.Instance.hitObject;

        RotateEventArgs args = new RotateEventArgs(trackedFinger1, trackedFinger2, angle, dir, hitObject);
        if (OnRotate != null)
            OnRotate(this, args);

        if(hitObject != null)
        {
            if(hitObject.TryGetComponent(out IRotatable rotatable))
            {
                rotatable.OnRotate(args);
            }
        }
        isPressed = true;
    }

    protected void OnDrawGizmos()
    {
        int touchCount = Input.touchCount;
        if(touchCount > 0)
        {
            for(int i = 0; i < Input.touches.Length; i++)
            {
                Touch t = Input.GetTouch(i);

                Ray r = Camera.main.ScreenPointToRay(t.position);

                switch (t.fingerId)
                {
                    case 0: Gizmos.DrawIcon(r.GetPoint(10), "Ayaka.png"); break;
                    case 1: Gizmos.DrawIcon(r.GetPoint(10), "Barbara.png"); break;
                    case 2: Gizmos.DrawIcon(r.GetPoint(10), "Keqing.png"); break;
                    case 3: Gizmos.DrawIcon(r.GetPoint(10), "Nigguang.png"); break;
                    case 4: Gizmos.DrawIcon(r.GetPoint(10), "Venti.png"); break;
                    default: Gizmos.DrawIcon(r.GetPoint(10), "Xiao.png"); break;
                }
            }
        }
    }
}
