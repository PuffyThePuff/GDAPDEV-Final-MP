using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    public float powerUp_duration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine (Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //PowerUp Functionality

        PowerUpFunctionality();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        
        yield return new WaitForSeconds(powerUp_duration);

        Destroy(gameObject);
    }

    public virtual void PowerUpFunctionality()
    {
        Debug.Log("Power Up Available");
    }
}
