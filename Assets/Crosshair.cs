using UnityEngine;
//using UnityEngine.UI;


public class Crosshair : MonoBehaviour
{
    [SerializeField] private float sensitivity = 10.0f;
    [SerializeField] private Vector2 border;
    [SerializeField] RectTransform crossHairTransform;

    RectTransform _canvasRectTransform;
    Joystick joystick;

    private Vector2 _position;
    private Vector2 _border;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        crossHairTransform = gameObject.GetComponent<RectTransform>();
        _canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //If the joystick is not touched
        if (joystick.Direction.x != 0.0f || joystick.Direction.y != 0.0f)
        {
            //Debug.Log(_canvasRectTransform.rect);
            #region Set the borders based on orientation
            float aspectRatio = Screen.width / (float)Screen.height;
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
            float x = (joystick.Direction.x * sensitivity * Time.fixedDeltaTime) + crossHairTransform.localPosition.x;
            float y = (joystick.Direction.y * sensitivity * Time.fixedDeltaTime) + crossHairTransform.localPosition.y;
#endregion
            //----------------------
#region Clamp direction to Screen
            _position.x = Mathf.Clamp(x,
                (
                    (_canvasRectTransform.rect.xMin)) + (crossHairTransform.rect.width / 2.0f) + _border.x,
                    (_canvasRectTransform.rect.xMax) - (crossHairTransform.rect.width / 2.0f) - _border.x
                );

            _position.y = Mathf.Clamp(y,
                (
                    (_canvasRectTransform.rect.yMin)) + (crossHairTransform.rect.height / 2.0f) + _border.y,
                    (_canvasRectTransform.rect.yMax) - (crossHairTransform.rect.height / 2.0f) - _border.y
                );
#endregion

            crossHairTransform.localPosition = _position;
        }
    }
}
