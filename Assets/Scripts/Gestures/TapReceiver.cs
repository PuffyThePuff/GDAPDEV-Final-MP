using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GestureManager.Instance.OnTap += OnTap;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTap -= OnTap;
    }

    private void OnTap(object sender, TapEventArgs tapEvent)
    {
        Ray r = Camera.main.ScreenPointToRay(tapEvent.TapPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
