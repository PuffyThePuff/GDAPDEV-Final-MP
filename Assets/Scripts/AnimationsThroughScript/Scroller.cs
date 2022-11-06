using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;

    //private Vector2 scrollSpeed = new Vector2();
    //private Vector2 position = new Vector2();
    private Rect scrollRect = new Rect();

    private void Start()
    {
        _img = GetComponent<RawImage>();
    }
    // Update is called once per frame
    void Update()
    {
        //scrollSpeed.x = _x;
        //scrollSpeed.y = _y;
        float posX = _img.uvRect.position.x + _x * Time.deltaTime;
        float posY = _img.uvRect.position.y + _y * Time.deltaTime;
        scrollRect.x = posX;
        scrollRect.y = posY;
        scrollRect.width = _img.uvRect.width;
        scrollRect.height = _img.uvRect.height;
        _img.uvRect = scrollRect;
    }
}
