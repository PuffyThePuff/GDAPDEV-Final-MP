using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.UI;


public class Crosshair : MonoBehaviour
{
    [SerializeField] private float sensitivity = 10.0f;
    [SerializeField] private Vector2 border;
    [Range(1.0f, 1000.0f)][SerializeField] private float range;
    [Range(1.0f, 1000.0f)][SerializeField] private float radius;
    [SerializeField] bool wideAim;

    RectTransform crossHairRectTransform;
    Transform crossHairTransform;
    //GameObject hitObj;

    RectTransform _canvasRectTransform;
    Joystick joystick;

    private Vector2 _position;
    private Vector2 _border;
    private float aspectRatio;

    private List<GameObject> hitObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        crossHairRectTransform = gameObject.GetComponent<RectTransform>();
        crossHairTransform = gameObject.transform;
        _canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        aspectRatio = Screen.width / (float)Screen.height;
        Move();
        CastRay();
    }

    private void CastRay()
    {
        RaycastHit[] hits = Physics.CapsuleCastAll(crossHairTransform.position, Vector3.forward * range, radius * aspectRatio, transform.forward, range);
        if(hits.Length != 0)
        {
            hitObjects.Clear();
            foreach (RaycastHit hit in hits)
            {
                if (!hitObjects.Contains(hit.collider.gameObject))
                {
                    hitObjects.Add(hit.collider.gameObject);
                }
            }
        }
    }
    private void Move()
    {
        //If the joystick is not touched
        if (joystick.Direction.x != 0.0f || joystick.Direction.y != 0.0f)
        {
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(crossHairTransform.position, Vector3.forward * radius);
    }
}
