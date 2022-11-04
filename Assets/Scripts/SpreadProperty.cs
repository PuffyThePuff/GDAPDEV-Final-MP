using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpreadProperty : MonoBehaviour
{
    [Tooltip("Min Change in Distance b/w the fingers to register pinch/spread")]
    public float MinDistanceChange = 0.2f;
}
