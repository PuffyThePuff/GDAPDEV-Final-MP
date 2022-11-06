using UnityEngine;

public enum CrosshairState { moving, stopped }
public class Crosshair : MonoBehaviour
{
    public static Crosshair Instance;
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

    [SerializeField] private float sensitivity = 10.0f;
    [SerializeField] private Vector2 border;
    [Range(1.0f, 1000.0f)][SerializeField] private float range;
    [Range(0.1f, 3.0f)][SerializeField] private float radius;
    [Range(0.1f, 10.0f)][SerializeField] private float scale;
    [SerializeField] bool wideAim;

    [Header("Manually Set if automatic is not working")]
    [SerializeField] RectTransform crossHairRectTransform;
    [SerializeField] Joystick joystick;

    //Canvas
    RectTransform _canvasRectTransform;

    //Temp values
    private Vector2 _position = new Vector2();
    private Vector2 _border = new Vector2();

    private float aspectRatio = 1.0f;
    private Vector2 scaleVector = new Vector2();

    //private List<GameObject> hitObjects = new List<GameObject>();
    public GameObject hitObject { get; private set; } = null;
    public CrosshairState CrosshairState { get; private set; } = CrosshairState.stopped;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        crossHairRectTransform = gameObject.GetComponent<RectTransform>();
        _canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        aspectRatio = (float)Screen.width / Screen.height;
        scaleVector.x = scale; scaleVector.y = scale;
        crossHairRectTransform.localScale = scaleVector;
        Move();
        CastRay();
    }

    private void CastRay()
    {
#if false
        RaycastHit[] hits = Physics.CapsuleCastAll(crossHairRectTransform.position, Vector3.forward * range, radius * aspectRatio * scale, transform.forward, range);
        hitObjects.Clear();
        if (hits.Length != 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (!hitObjects.Contains(hit.collider.gameObject))
                {
                    hitObjects.Add(hit.collider.gameObject);
                }
            }
        }
#endif
#if false
        Ray r = Camera.main.ScreenPointToRay(crossHairRectTransform.position);
        RaycastHit hit;
        if(Physics.Raycast(r, out hit, range))
        {
            hitObject = hit.collider.gameObject;
            Debug.Log(hitObject);
        }
#endif
        RaycastHit hit;
        if (Physics.Raycast(crossHairRectTransform.position, Vector3.forward, out hit, range))
        {
            hitObject = hit.collider.gameObject;
            Debug.Log(hitObject);
        }
        else
        {
            hitObject = null;
        }
    }
    private void Move()
    {
        //If the joystick is not touched
        if (joystick.Direction.x != 0.0f || joystick.Direction.y != 0.0f)
        {
            CrosshairState = CrosshairState.moving;
            //Debug.Log(_canvasRectTransform.rect);
#region Set the borders based on orientation
            //Debug.Log(aspectRatio);
#if true
            if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                //Borders are the same
                _border.x = border.x * aspectRatio;
                _border.y = border.y * aspectRatio;
            }
            else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                //Switch the borders
                _border.x = border.y * aspectRatio;
                _border.y = border.x * aspectRatio;
            }
#endif
#endregion
            //----------------------
#region Set coordinates for the crosshair position
            float x = (joystick.Direction.x * sensitivity * Time.fixedDeltaTime) + crossHairRectTransform.localPosition.x;
            float y = (joystick.Direction.y * sensitivity * Time.fixedDeltaTime) + crossHairRectTransform.localPosition.y;
#endregion
            //----------------------
#region Clamp direction to Screen
            _position.x = Mathf.Clamp(x,
                (
                    (_canvasRectTransform.rect.xMin)) + (crossHairRectTransform.rect.width / 2.0f) + _border.x,
                    (_canvasRectTransform.rect.xMax) - (crossHairRectTransform.rect.width / 2.0f) - _border.x
                );

            _position.y = Mathf.Clamp(y,
                (
                    (_canvasRectTransform.rect.yMin)) + (crossHairRectTransform.rect.height / 2.0f) + _border.y,
                    (_canvasRectTransform.rect.yMax) - (crossHairRectTransform.rect.height / 2.0f) - _border.y
                );
#endregion

            crossHairRectTransform.localPosition = _position;
        }
        else
        {
            CrosshairState = CrosshairState.stopped;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawLine(crossHairRectTransform.position, Vector3.forward * range);
        Gizmos.DrawWireSphere(crossHairRectTransform.position, radius * aspectRatio * scale);
    }
}
