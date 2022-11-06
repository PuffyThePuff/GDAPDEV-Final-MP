using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;

    private Vector2 scrollSpeed = new Vector2();

    private void Start()
    {
        _img = GetComponent<RawImage>();
    }
    // Update is called once per frame
    void Update()
    {
        scrollSpeed.x = _x;
        scrollSpeed.y = _y;
        _img.uvRect = new Rect(_img.uvRect.position + scrollSpeed * Time.deltaTime, _img.uvRect.size);
    }
}
