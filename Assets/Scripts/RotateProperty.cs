using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RotateProperty : MonoBehaviour
{
    [Tooltip("Min Distance b/w the fingers")]
    public float minDistance = 0.75f;
    [Tooltip("Min Change of Axis/Angle")]
    public float minChange = 0.4f;
}
