using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    //Called in Awake
    #region Singleton
    public static CameraHandler instance { get; private set; }
    public void InstantiateSingleton()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    private Camera Camera;
    public Transform cam_Transform { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    {
        InstantiateSingleton();
        Camera = Camera.main;
        cam_Transform = Camera.transform;
    }

    public Vector2 CalculateScreenToWorldView()
    {
        Vector3 position = new Vector3(Screen.width, Screen.height, cam_Transform.position.z);
        //Debug.Log(Camera.ScreenToWorldPoint(position));
        return Camera.ScreenToWorldPoint(position);
    }
}
