using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField]
    public int curr_value = 5;
    private BoxCollider2D myboxCollider;
    private SpriteRenderer myspriteRenderer;

    private void Start()
    {
        myboxCollider = GetComponent<BoxCollider2D>();
        myspriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpCurrency();
        }
    }

    private void PickUpCurrency()
    {
        // Currency Calculations

        // Destroy Currency after collision
        Destroy(gameObject);

    }


}
