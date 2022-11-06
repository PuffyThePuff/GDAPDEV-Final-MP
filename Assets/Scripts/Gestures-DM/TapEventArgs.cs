using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TapEventArgs : EventArgs
{

    private Vector2 _tapPosition;
    private GameObject _tapObject;

    public TapEventArgs(Vector2 tapPosition, GameObject tapObject)
    {
        _tapPosition = tapPosition;
        _tapObject = tapObject;
    }

    public Vector2 TapPosition
    {
        get
        {
            return _tapPosition;
        }
    }
    public GameObject TapObject
    {
        get { return _tapObject; }
    }
}
