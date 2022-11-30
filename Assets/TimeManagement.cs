using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagement : MonoBehaviour
{
    public void AdjustTime(float time)
    {
        Time.timeScale = time;
    }
}
