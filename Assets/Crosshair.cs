using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private float sensitivity = 10.0f;
    [SerializeField] private Vector2 border;

    RectTransform crossHairTransform;
    Joystick joystick;

    Vector2 _direction;
    Vector2 _border;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        crossHairTransform = gameObject.GetComponent<RectTransform>();
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
            #region Set the borders based on orientation
            if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                //Borders are the same
                _border.x = border.x;
                _border.y = border.y;
            }
            else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                //Switch the borders
                _border.x = border.y;
                _border.y = border.x;
            }
            #endregion
            #region Set coordinates for the crosshair position
            float x = (joystick.Direction.x * sensitivity * Time.fixedDeltaTime) + crossHairTransform.localPosition.x;
            float y = (joystick.Direction.y * sensitivity * Time.fixedDeltaTime) + crossHairTransform.localPosition.y;
            #endregion
            #region Clamp direction to Screen
            _direction.x = Mathf.Clamp(x,
                (
                    -(Screen.width / 2)) + crossHairTransform.rect.width + _border.x,
                    (Screen.width / 2) - crossHairTransform.rect.width - _border.x
                );

            _direction.y = Mathf.Clamp(y,
                (
                    -(Screen.height / 2)) + crossHairTransform.rect.height + _border.y,
                    (Screen.height / 2) - crossHairTransform.rect.height - _border.y
                );
            #endregion

            crossHairTransform.localPosition = _direction;
        }
    }
}
